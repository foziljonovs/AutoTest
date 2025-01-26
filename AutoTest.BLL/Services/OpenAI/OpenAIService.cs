using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.Services.OpenAI;

public class OpenAIService(
    ITestService testService,
    IQuestionService questionService,
    HttpClient client,
    IConfiguration configuration,
    IMapper mapper) : IOpenAIService
{
    private readonly ITestService _testService = testService;
    private readonly IQuestionService _questionService = questionService;
    private readonly HttpClient _client = client;
    private readonly string _apiKey = configuration["OpenAI:ApiKey"];
    private readonly IMapper _mapper = mapper;

    public async Task<long> CompleteAsync(GenerateTestDto dto)
    {
        try
        {
            if(dto != null)
            {
                var generatedTest = await GenerateAsync(dto);
                if (!string.IsNullOrEmpty(generatedTest))
                {
                    var test = new TestDto
                    {
                        Title = dto.Title,
                        Description = dto.Description,
                        Level = dto.Level,
                        UserId = dto.UserId
                    };

                    var convertedTest = await ConvertToTest(generatedTest, test, dto.UserId);
                    if (convertedTest != null)
                    {
                        var createTest = _mapper.Map<CreateTestDto>(convertedTest);
                        var testId = await _testService.AddTestAsync(createTest);

                        if (testId > 0)
                        {
                            var questions = convertedTest.Question;
                            foreach (var question in questions)
                            {
                                question.TestId = testId;
                                var createQuestion = _mapper.Map<CreateQuestionDto>(question);
                                await _questionService.AddAsync(createQuestion);
                            }
                            return testId;
                        }
                        else
                        {
                            throw new Exception("An error occurred while saving the test.");
                        }
                    }
                    else
                    {
                        throw new Exception("The converted test is empty.");
                    }
                }
                else
                {
                    throw new Exception("The generated test is empty.");
                }
            }
            else
            {
                throw new Exception("The test data is empty.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while completing the test: {ex.Message}");
        }
    }
    public async Task<TestDto> ConvertToTest(string content, TestDto dto, long userId)
    {
        try
        {
            if(!string.IsNullOrEmpty(content))
            {
                var requestBody = new
                {
                    model = "text-davinci-003",
                    prompt = $"Convert the following {content} into a structured {dto} object. " +
                        $"Include Title, Description, Level, Status, and a list of questions. Each question should be mapped into a QuestionDto with Problem, Type, and Options (with IsCorrect flag). Exclude Id, CreatedDate, and UpdatedDate.",
                    max_tokens = 300,
                    temperature = 0.5
                };

                var request = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var response = await _client.PostAsync("https://api.openai.com/v1/completions", request);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"OpenAI API error : {error}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<string>(responseContent);

                var test = _mapper.Map<TestDto>(result);

                return test;
            }
            else
            {
                throw new Exception("The generated test is empty.");
            }
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occurred while converting the generated test: {ex.Message}");
        }
    }

    public async Task<string> GenerateAsync(GenerateTestDto dto)
    {
        try
        {
            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = $"Generate a test on {dto.Title} with {dto.QuestionCount} questions of {dto.Level} level.",
                max_tokens = 300,
                temperature = 0.5
            };

            var request = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _client.PostAsync("https://api.openai.com/v1/completions", request);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API error : {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<string>(responseContent);

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occurred while generating the test: {ex.Message}");
        }
    }
}
