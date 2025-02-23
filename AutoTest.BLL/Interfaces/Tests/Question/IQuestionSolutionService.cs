using AutoTest.BLL.DTOs.Tests.QuestionSolution;

namespace AutoTest.BLL.Interfaces.Tests.Question;

public interface IQuestionSolutionService
{
    Task<IEnumerable<QuestionSolutionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<QuestionSolutionDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(CreateQuestionSolutionDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId, CancellationToken cancellationToken = default);
}
