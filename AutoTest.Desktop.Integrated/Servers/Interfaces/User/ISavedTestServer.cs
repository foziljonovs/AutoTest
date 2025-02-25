using AutoTest.BLL.DTOs.Users.SavedTest;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.User;

public interface ISavedTestServer
{
    Task<List<SavedTestDto>> GetAllAsync();
    Task<SavedTestDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreatedSavedTestDto dto);
    Task<bool> DeleteAsync(long id);
    Task<List<SavedTestDto>> GetAllByUserIdAsync(long userId);
    Task<List<SavedTestDto>> GetAllByTestIdAsync(long testId);
}
