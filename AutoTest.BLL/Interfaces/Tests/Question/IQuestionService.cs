using AutoTest.BLL.DTOs.Tests.Question;
using System.Diagnostics;

namespace AutoTest.BLL.Interfaces.Tests.Question;

public interface IQuestionService
{
    Task<IEnumerable<QuestionDto>> GetAllAsync(CancellationToken cancellation = default);
    Task<QuestionDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<long> AddAsync(CreateQuestionDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateQuestionDto dto, CancellationToken cancellation = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
    Task<IEnumerable<QuestionDto>> GetQuestionsByTestAsync(long testId, CancellationToken cancellation = default);
}
