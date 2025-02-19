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
    public async Task<List<UserDto>> GetAllAsync()
    {
        try
        {
            if(IsInternetAvailable())
            {
                var response = await _server.GetAllAsync();
                return response;
            }
            else
            {
                return new List<UserDto>();
            }
        }
        catch(Exception ex)
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
