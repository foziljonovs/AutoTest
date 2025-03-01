using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using AutoTest.Desktop.Integrated.Servers.Repositories.Test;

namespace AutoTest.Desktop.Integrated.Services.Test;

public class TestSolutionService : ITestSolutionService
{
    private readonly ITestSolutionServer _server;
    public TestSolutionService()
    {
        this._server = new TestSolutionServer();
    }
    public async Task<long> AddAsync(CreateTestSolutionDto createTestSolutionDto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.AddAsync(createTestSolutionDto);
            else
                return -1;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public async Task<List<TestSolutionDto>> GetAllAsync()
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.GetAllAsync();
            else
                return new List<TestSolutionDto>();
        }
        catch(Exception ex)
        {
            return new List<TestSolutionDto>();
        }
    }

    public async Task<TestSolutionDto> GetByIdAsync(long id)
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.GetByIdAsync(id);
            else
                return new TestSolutionDto();
        }
        catch(Exception ex)
        {
            return new TestSolutionDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.UpdateAsync(id, updateTestSolutionDto);
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
