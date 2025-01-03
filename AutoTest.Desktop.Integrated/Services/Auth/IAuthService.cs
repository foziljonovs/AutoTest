using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Services.Auth;

public interface IAuthService
{
    Task<(bool result, string token)> LoginAsync(LoginDto dto, CancellationToken cancellation = default);
    Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default);
}
