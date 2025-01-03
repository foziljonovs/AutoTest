using AutoTest.BLL.DTOs.User;

namespace AutoTest.Desktop.Integrated.Services.Auth;

public class AuthService(IAuthService server) : IAuthService
{
    private readonly IAuthService _server = server;
    public async Task<(bool result, string token)> LoginAsync(LoginDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var res = await _server.LoginAsync(dto, cancellation);

            if(res.result)
            {
                return (true, res.token);
            }
            else
            {
                return (false, string.Empty);
            }
        }
        catch (Exception ex)
        {
            return (false, string.Empty);
        }
    }

    public async Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var res = await _server.RegisterAsync(dto, cancellation);

            if (res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
