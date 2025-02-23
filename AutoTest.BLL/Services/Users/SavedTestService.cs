using AutoTest.BLL.DTOs.Users.SavedTest;
using AutoTest.BLL.Interfaces.Users;

namespace AutoTest.BLL.Services.Users;

public class SavedTestService : ISavedTestService
{
    public Task<bool> AddAsync(CreatedSavedTestDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SavedTestDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SavedTestDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SavedTestDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SavedTestDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
