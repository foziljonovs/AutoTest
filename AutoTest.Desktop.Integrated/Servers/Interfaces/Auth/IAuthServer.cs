using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Auth;

public interface IAuthServer
{
    Task<(bool result, string token)> LoginAsync(LoginDto dto, CancellationToken cancellation = default);
    Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default);
}
