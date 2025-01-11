using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using AutoTest.Desktop.Integrated.Servers.Repositories.Test;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace AutoTest.Desktop.Integrated.Services.Test;

public class TestService : ITestService
{
    private readonly ITestServer _server;
    public TestService()
    {
        this._server = new TestServer();
    }

    public async Task<bool> AddAsync(CreateTestDto dto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.AddAsync(dto);
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<TestDto>> CompletedTaskAsync()
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.CompletedTaskAsync();
            else
                return new List<TestDto>();
        }
        catch (Exception ex)
        {
            return new List<TestDto>();
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            return true;
            //if (IsInternetAvailable())
            //    return await _server.DeleteAsync(id);
            //else
            //    return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<TestDto>> GetAllAsync()
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.GetAllAsync();
            else
                return new List<TestDto>();
        }
        catch(Exception ex)
        {
            return new List<TestDto>();
        }
    }

    public async Task<TestDto> GetByIdAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetByIdAsync(id);
            else
                return new TestDto();
        }
        catch(Exception ex)
        {
            return new TestDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestDto dto)
    {
        try
        {
            if (IsInternetAvailable())
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
            using(Ping ping = new Ping())
            {
                PingReply reply = ping.Send("www.google.com");
                return (reply.Status == IPStatus.Success);
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
