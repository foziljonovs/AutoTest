using AutoTest.BLL.DTOs.Users.SavedTest;

namespace AutoTest.BLL.Interfaces.Users;

public interface ISavedTestService
{
    Task<IEnumerable<SavedTestDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SavedTestDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(CreatedSavedTestDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
    Task<IEnumerable<SavedTestDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<SavedTestDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default);
}
