using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using AutoTest.Desktop.Integrated.Servers.Repositories.User;

namespace AutoTest.Desktop.Integrated.Services.User;

public class UserService : IUserService
{
    private readonly IUserServer _server;
    public UserService()
    {
        this._server = new UserServer();
    }

    public async Task<bool> ChangePasswordAsync(long id, UserChangePasswordDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                return await _server.ChangePasswordAsync(id, dto);
            }
            else
            {
                return false;
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
            {
                return await _server.DeleteAsync(id);
            }
            else
            {
                return false;
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        try
        {
            if (IsInternetAvailable())
            {
                var response = await _server.GetAllAsync();
                return response;
            }
            else
            {
                return new List<UserDto>();
            }
        }
        catch (Exception ex)
        {
            return new List<UserDto>();
        }
    }

    public async Task<UserDto> GetByIdAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var response = await _server.GetByIdAsync(id);
                return response;
            }
            else
            {
                return new UserDto();
            }
        }
        catch (Exception ex)
        {
            return new UserDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateUserDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                return await _server.UpdateAsync(id, dto);
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

    public async Task<bool> VerifyPasswordAsync(long id, string password)
    {
        try
        {
            if (IsInternetAvailable())
            {
                return await _server.VerifyPasswordAsync(id, password);
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

    private bool IsInternetAvailable()
    {
        try
        {
            return true;
            //using(Ping ping = new Ping())
            //{
            //    PingReply reply = ping.Send("www.google.com");
            //    return (reply.Status == IPStatus.Success);
            //}
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
