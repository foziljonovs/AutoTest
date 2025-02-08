using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text.Json;
using System.Text.RegularExpressions;

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
            Tools =
            [
                AIFunctionFactory.Create((Func<GenerateTestDto, Task<string>>)GenerateAsync),
                AIFunctionFactory.Create((Func<string, TestDto, long, Task<TestDto>>)ConvertToTest)
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
    public async Task<TestDto> ConvertToTest(string content, TestDto dto, long userId)
    {
        try
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("The generated test content is empty.", nameof(content));
            }

            var chatMessages = new ChatHistory
            {
                new ChatMessageContent(AuthorRole.System, "Convert the generated test into a structured TestDto object."),
                new ChatMessageContent(AuthorRole.User,
                    "Convert the following test content into a structured JSON object. " +
                    "The JSON object must strictly follow this format:\n" +
                    "{\n" +
                    "  \"title\": \"Test title\",\n" +
                    "  \"description\": \"Test description\",\n" +
                    "  \"level\": \"Test level\",\n" +
                    "  \"status\": \"Active\",\n" +
                    "  \"questions\": [\n" +
                    "    {\n" +
                    "      \"problem\": \"Question problem\",\n" +
                    "      \"type\": \"MultipleChoice\",\n" +
                    "      \"options\": [\n" +
                    "        { \"text\": \"A Text\", \"isCorrect\": false\true },\n" +
                    "        { \"text\": \"B Text\", \"isCorrect\": false\true }\n" +
                    "      ]\n" +
                    "    }\n" +
                    "  ]\n" +
                    "} \n" +
                    "Return only valid JSON, without any extra explanations."
                )
            };

            var response = await _client.CompleteAsync(content, _chatOptions, cancellationToken: default);

            if (response?.Choices == null || response.Choices.Count == 0 || response.Choices[0].Contents == null)
            {
                throw new Exception("AI service did not return a valid response.");
            }

            var convertToResponse = response.Choices[0].Contents.ToString().Trim();

            if (!convertToResponse.StartsWith("{") || !convertToResponse.EndsWith("}"))
            {
                Console.WriteLine("AI Response: " + convertToResponse);
                throw new Exception("AI did not return a valid JSON format.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            var testDto = JsonSerializer.Deserialize<TestDto>(convertToResponse, options);

            if (testDto == null)
            {
                throw new Exception("Deserialized object is null.");
            }

            return testDto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// AI yordamida yangi test generatsiya qilish
    /// </summary>
    public async Task<string> GenerateAsync(GenerateTestDto dto)
    {
        try
        {
            var message = $"Generate a test on {dto.Title} with {dto.QuestionCount} questions of {dto.Level} level.";
            var response = await _client.CompleteAsync(
                message,
                _chatOptions,
                cancellationToken: default);

            if (response?.Choices == null || response.Choices.Count == 0)
            {
                throw new Exception("AI service did not return a valid response.");
            }

            var contentList = response.Choices[0].Contents;

            if (contentList == null || contentList.Count == 0)
            {
                throw new Exception("AI response contains no content.");
            }

            var generatedContent = contentList[0].ToString();

            return generatedContent;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while generating test: {ex.Message}");
        }
    }
}
