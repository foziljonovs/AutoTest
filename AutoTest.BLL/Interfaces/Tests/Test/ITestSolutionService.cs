using AutoTest.BLL.DTOs.Tests.TestSolution;

namespace AutoTest.BLL.Interfaces.Tests.Test;

public interface ITestSolutionService
{
    Task<IEnumerable<TestSolutionDto>> GetAllAsync(CancellationToken cancellation = default);
    Task<TestSolutionDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<bool> AddAsync(CreateTestSolutionDto createTestSolutionDto, CancellationToken cancellation = default);
    Task<long> AddTestSolutionAsync(CreateTestSolutionDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto, CancellationToken cancellation = default);
}
