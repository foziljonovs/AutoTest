using AutoTest.BLL.DTOs.Tests.QuestionSolution;
using AutoTest.BLL.Interfaces.Tests.Question;

namespace AutoTest.BLL.Services.Tests.Question;

public class QuestionSolutionService : IQuestionSolutionService
{
    public Task<bool> AddAsync(CreateQuestionSolutionDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QuestionSolutionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionSolutionDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
