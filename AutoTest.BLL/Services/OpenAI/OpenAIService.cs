using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AutoTest.BLL.Services.OpenAI;

public class OpenAIService : IOpenAIService
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly IMapper _mapper;

    public OpenAIService(
        ITestService testService,
        IQuestionService questionService,
        HttpClient client,
        IConfiguration configuration,
        IMapper mapper)
    {
        _testService = testService;
        _questionService = questionService;
        _client = client;
        _apiKey = configuration["OpenAI:ApiKey"];
        _mapper = mapper;
    }

    public async Task<long> CompleteAsync(GenerateTestDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "The test data is empty.");
        }

        var generatedTest = await GenerateAsync(dto);
        if (string.IsNullOrEmpty(generatedTest))
        {
            throw new Exception("The generated test is empty.");
        }

        var test = new TestDto
        {
            Title = dto.Title,
            Description = dto.Description,
            Level = dto.Level,
            UserId = dto.UserId
        };

        var convertedTest = await ConvertToTest(generatedTest, test, dto.UserId);
        if (convertedTest == null)
        {
            throw new Exception("The converted test is empty.");
        }

        var createTest = _mapper.Map<CreateTestDto>(convertedTest);
        var testId = await _testService.AddTestAsync(createTest);

        if (testId <= 0)
        {
            throw new Exception("An error occurred while saving the test.");
        }

        foreach (var question in convertedTest.Question)
        {
            question.TestId = testId;
            var createQuestion = _mapper.Map<CreateQuestionDto>(question);
            await _questionService.AddAsync(createQuestion);
        }

        return testId;
    }

    public async Task<TestDto> ConvertToTest(string content, TestDto dto, long userId)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("The generated test content is empty.", nameof(content));
        }

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "developer", content = "Convert the generated test to a test object." },
                new { role = "user", content = $"Convert the following content into a structured object: {content}. Include Title, Description, Level, Status, and a list of questions. Each question should be mapped into a QuestionDto with Problem, Type, and Options (with IsCorrect flag). Exclude Id, CreatedDate, and UpdatedDate." }
            },
            max_tokens = 300,
            temperature = 0.5
        };

        var request = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _client.PostAsync("https://api.openai.com/v1/completions", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"OpenAI API error: {error}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);
        var convertedContent = result.GetProperty("choices")[0].GetProperty("text").GetString();

        return _mapper.Map<TestDto>(convertedContent);
    }

    public async Task<string> GenerateAsync(GenerateTestDto dto)
    {
        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "developer", content = "Generate a test." },
                new { role = "user", content = $"Generate a test on {dto.Title} with {dto.QuestionCount} questions of {dto.Level} level." }
            },
            max_tokens = 300,
            temperature = 0.5
        };

        var request = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _client.PostAsync("https://api.openai.com/v1/chat/completions", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"OpenAI API error: {error}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "No content generated.";
    }
}