using AutoMapper;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AutoTest.BLL.Services.OpenAI;

public class OpenAIService : IOpenAIService
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly IChatCompletionService _chatCompleteService;
    private readonly Kernel _kernel;
    private readonly IMapper _mapper;

    public OpenAIService(
        ITestService testService,
        IQuestionService questionService,
        Kernel kernel,
        IChatCompletionService chatCompleteService,
        IMapper mapper)
    {
        _testService = testService;
        _questionService = questionService;
        _mapper = mapper;
        _kernel = kernel;
        _chatCompleteService = chatCompleteService;
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

        var chatMessages = new ChatHistory
        {
            new ChatMessageContent(AuthorRole.System, "Convert the generated test into a structured TestDto object."),
            new ChatMessageContent(AuthorRole.User, $"Convert the following content into a structured object: {content}. Include Title, Description, Level, Status, and a list of questions. Each question should be mapped into a QuestionDto with Problem, Type, and Options (with IsCorrect flag). Exclude Id, CreatedDate, and UpdatedDate.")
        };

        var response = await _chatCompleteService.GetChatMessageContentAsync(chatMessages);
        return _mapper.Map<TestDto>(response?.Content);
    }

    public async Task<string> GenerateAsync(GenerateTestDto dto)
    {
        var chatMessages = new ChatHistory
        {
            new ChatMessageContent(AuthorRole.System, "Generate a test."),
            new ChatMessageContent(AuthorRole.User, $"Generate a test on {dto.Title} with {dto.QuestionCount} questions of {dto.Level} level.")
        };

        var response = await _chatCompleteService.GetChatMessageContentAsync(chatMessages);
        return response?.Content ?? string.Empty;
    }
}
