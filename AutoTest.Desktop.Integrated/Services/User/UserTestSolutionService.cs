using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using AutoTest.Desktop.Integrated.Servers.Repositories.User;

namespace AutoTest.Desktop.Integrated.Services.User;

public class UserTestSolutionService : IUserTestSolutionService
{
    private readonly IUserTestSolutionServer _server;
    public UserTestSolutionService()
    {
        this._server = new UserTestSolutionServer();
    }
    public async Task<bool> AddAsync(CreateUserTestSolutionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.AddAsync(dto);
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<UserTestSolutionDto>> GetAllByTestIdAsync(long testId)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetAllByTestIdAsync(testId);
            else
                return new List<UserTestSolutionDto>();
        }
        catch(Exception ex)
        {
            return new List<UserTestSolutionDto>();
        }
    }

    public async Task<List<UserTestSolutionDto>> GetAllByUserIdAsync(long userId)
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.GetAllByUserIdAsync(userId);
            else
                return new List<UserTestSolutionDto>();
        }
        catch(Exception ex)
        {
            return new List<UserTestSolutionDto>();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateUserTestSolutionDto dto)
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.UpdateAsync(id, dto);
            else
                return false;
        }
        catch(Exception ex)
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
