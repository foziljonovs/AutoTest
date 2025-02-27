using AutoTest.BLL.DTOs.Tests.TestSolution;

namespace AutoTest.Desktop.Integrated.Services.Test;

public interface ITestSolutionService
{
    Task<List<TestSolutionDto>> GetAllAsync();
    Task<TestSolutionDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreateTestSolutionDto createTestSolutionDto);
    Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto);
}
