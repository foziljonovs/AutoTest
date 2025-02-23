using AutoTest.BLL.DTOs.Users.User;

namespace AutoTest.BLL.Interfaces.Users;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellation = default);
    Task<UserDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default);
    Task<string> LoginAsync(LoginDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateUserDto dto, CancellationToken cancellation = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
    Task<bool> ChangePasswordAsync(long id, UserChangePasswordDto dto, CancellationToken cancellation = default);
    Task<bool> VerifyPasswordAsync(long id, string password, CancellationToken cancellation = default);
}
