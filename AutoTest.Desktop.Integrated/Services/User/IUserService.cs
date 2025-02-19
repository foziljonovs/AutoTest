using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Services.User;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(long id);
}
