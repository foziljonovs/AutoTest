using AutoTest.BLL.DTOs.Tests.Question;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Question;

public interface IQuestionServer
{
    Task<List<QuestionDto>> GetAllAsync();
    Task<long> AddAsync(CreateQuestionDto dto);
    Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId);
    Task<bool> UpdateAsync(long id, UpdateQuestionDto dto);
    Task<bool> DeleteAsync(long id);
}
