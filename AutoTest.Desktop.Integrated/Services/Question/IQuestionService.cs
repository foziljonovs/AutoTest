using AutoTest.BLL.DTOs.Tests.Question;

namespace AutoTest.Desktop.Integrated.Services.Question;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetAllAsync();
    Task<bool> AddAsync(CreateQuestionDto dto);
    Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId);
}
