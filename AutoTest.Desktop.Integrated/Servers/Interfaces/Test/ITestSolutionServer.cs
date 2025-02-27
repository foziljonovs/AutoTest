using AutoTest.BLL.DTOs.Tests.TestSolution;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Test;

public interface ITestSolutionServer
{
    Task<List<TestSolutionDto>> GetAllAsync();
    Task<TestSolutionDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreateTestSolutionDto createTestSolutionDto);
    Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto);
}
