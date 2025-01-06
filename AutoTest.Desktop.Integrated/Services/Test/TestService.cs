using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace AutoTest.Desktop.Integrated.Services.Test;

public class TestService : ITestService
{
    private readonly ITestServer _server;
    public TestService(ITestServer server)
    {
        this._server = server;
    }
    public async Task<List<TestDto>> GetAllAsync()
    {
        try
        {
            if(IsInternetAvailable())
            {
                var tests = await _server.GetAllAsync();
                return tests;
            }
            else
            {
                return new List<TestDto>();
            }
        }
        catch(Exception ex)
        {
            return new List<TestDto>();
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
