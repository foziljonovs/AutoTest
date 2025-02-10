using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.Domain.Enums;
using Microsoft.Extensions.AI;
using System.Text.Json;

namespace AutoTest.BLL.Services.OpenAI;

public class OpenAIService : IOpenAIService
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly IChatClient _client;
    private readonly ChatOptions _chatOptions;
    private readonly IMapper _mapper;

    public OpenAIService(
        ITestService testService,
        IQuestionService questionService,
        IChatClient client,
        IMapper mapper)
    {
        _testService = testService;
        _questionService = questionService;
        _client = client;
        _mapper = mapper;

        _chatOptions = new ChatOptions
        {
            MaxOutputTokens = 1000,
            Temperature = 0.6f,
            StopSequences = ["```"],
            Tools =
            [
                AIFunctionFactory.Create((Func<TestDto, long, int, Task<TestDto>>)GenerateAsync)
            ]
        };
    }

    /// <summary>
    /// AI yordamida test generatsiya qilish va bazaga saqlash
    /// </summary>
    public async Task<long> CompleteAsync(GenerateTestDto dto)
    {
        try
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "The test data is empty.");
            }

            var test = new TestDto
            {
                Title = dto.Title,
                Description = dto.Description,
                Level = dto.Level,
                Status = TestStatus.InProcess,
                UserId = dto.UserId
            };

            var convertedTest = await GenerateAsync(test, dto.UserId, dto.QuestionCount);
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

            if (convertedTest.Question == null || !convertedTest.Question.Any())
            {
                throw new Exception("The test does not contain any questions.");
            }

            foreach (var question in convertedTest.Question)
            {
                question.TestId = testId;
                var createQuestion = _mapper.Map<CreateQuestionDto>(question);
                await _questionService.AddAsync(createQuestion);
            }

            return testId;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// AI yordamida testni TestDto formatiga o‘tkazish
    /// </summary>
    public async Task<TestDto> GenerateAsync(TestDto dto, long userId, int count)
    {
        try
        {
            var chatMessages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, $"Generate a test on {dto.Title} {dto.Description} with {count} questions of {dto.Level} level."),
                new ChatMessage(ChatRole.System, "Return only valid JSON response without explanations."),
                new ChatMessage(ChatRole.System, @"{
                    ""title"": ""Sample Test"",
                    ""description"": ""Test description"",
                    ""level"": 1,
                    ""status"": 1,
                    ""question"": [
                        { ""problem"": ""What is C#?"", ""type"": ""multiple_choice"", ""options"": [
                            { ""text"": ""A programming language"", ""isCorrect"": true },
                            { ""text"": ""A database"", ""isCorrect"": false }
                        ] }
                    ]
                }")
            };

            var response = await _client.CompleteAsync(chatMessages, _chatOptions, cancellationToken: default);

            var generatedContent = response.Choices[0].ToString().Trim();

            int startIndex = generatedContent.IndexOf('{');
            int endIndex = generatedContent.LastIndexOf('}');

            if (startIndex == -1 || endIndex == -1 || startIndex >= endIndex)
            {
                throw new Exception("AI did not return a valid JSON format.");
            }

            generatedContent = generatedContent.Substring(startIndex, endIndex - startIndex + 1);


            if (string.IsNullOrWhiteSpace(generatedContent) || !generatedContent.StartsWith("{") || !generatedContent.EndsWith("}"))
                throw new Exception("AI response is not a valid JSON format.");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var testDto = JsonSerializer.Deserialize<TestDto>(generatedContent, options);
            if (testDto?.Question == null || !testDto.Question.Any())
                throw new Exception("Deserialized object is invalid or does not contain questions.");

            testDto.Question.ForEach(q => q.Id = 0);
            return testDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while converting test: " + ex.Message, ex);
        }
    }
}
