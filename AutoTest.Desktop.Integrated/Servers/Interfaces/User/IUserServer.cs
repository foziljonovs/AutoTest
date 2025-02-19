using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.User;

public interface IUserServer
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(long id);
}
