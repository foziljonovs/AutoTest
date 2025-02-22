using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Services.User;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(long id);
    Task<bool> UpdateAsync(long id, UpdateUserDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> ChangePasswordAsync(long id, UserChangePasswordDto dto);
    Task<bool> VerifyPasswordAsync(long id, string password);
}
