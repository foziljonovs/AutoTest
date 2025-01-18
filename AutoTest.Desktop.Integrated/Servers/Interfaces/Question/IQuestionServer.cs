using AutoTest.BLL.DTOs.Tests.Question;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Question;

public interface IQuestionServer
{
    Task<List<QuestionDto>> GetAllAsync();
    Task<bool> AddAsync(CreateQuestionDto dto);
    Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId);
}
