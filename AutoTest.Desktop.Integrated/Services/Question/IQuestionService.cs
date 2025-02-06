using AutoTest.BLL.DTOs.Tests.Question;

namespace AutoTest.Desktop.Integrated.Services.Question;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetAllAsync();
    Task<long> AddAsync(CreateQuestionDto dto);
    Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId);
    Task<bool> DeleteAsync(long id);
    Task<bool> UpdateAsync(long id, UpdateQuestionDto dto);
}
