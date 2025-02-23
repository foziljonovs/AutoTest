using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.BLL.Interfaces.Tests.Test;

namespace AutoTest.BLL.Services.Tests.Test;

public class TestSolutionService : ITestSolutionService
{
    public Task<bool> AddAsync(CreateTestSolutionDto createTestSolutionDto, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TestSolutionDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<TestSolutionDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
