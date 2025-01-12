using AutoTest.BLL.DTOs.Tests.Test;

namespace AutoTest.Desktop.Integrated.Services.Test;

public interface ITestService
{
    Task<List<TestDto>> GetAllAsync();
    Task<TestDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreateTestDto dto);
    Task<bool> UpdateAsync(long id, UpdateTestDto dto);
    Task<bool> DeleteAsync(long id);
    Task<List<TestDto>> CompletedTaskAsync();
    Task<List<TestDto>> GetAllByUserIdAsync(long userId);
}
