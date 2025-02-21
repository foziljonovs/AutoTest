using AutoTest.BLL.DTOs.Tests.Test;

namespace AutoTest.BLL.Interfaces.Tests.Test;

public interface ITestService
{
    Task<IEnumerable<TestDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TestDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<bool> AddAsync(CreateTestDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateTestDto dto, CancellationToken cancellation = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
    Task<IEnumerable<TestDto>> GetAllCompletedAsync(CancellationToken cancellation = default);
    Task<IEnumerable<TestDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellation = default);
    Task<long> AddTestAsync(CreateTestDto dto, CancellationToken cancellation = default);
    Task<IEnumerable<TestDto>> GetAllByTopicIdAsync(long topicId, CancellationToken cancellation = default);
}
