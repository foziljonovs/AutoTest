using AutoTest.BLL.DTOs.Users.SavedTest;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.User;

public interface ISavedTestServer
{
    Task<List<SavedTestDto>> GetAllAsync();
    Task<SavedTestDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreatedSavedTestDto dto);
}
