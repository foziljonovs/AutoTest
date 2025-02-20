using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Option;
using AutoTest.Desktop.Integrated.Servers.Repositories.Option;

namespace AutoTest.Desktop.Integrated.Services.Option;

public class OptionService : IOptionService
{
    private readonly IOptionServer _server;
    public OptionService()
    {
        this._server = new OptionServer();
    }
    public async Task<long> AddAsync(CreateOptionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.AddAsync(dto);
                return res;
            }
            else
            {
                return -1;
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var res = await _server.DeleteAsync(id);
            return res;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<OptionDto>> GetAllAsync()
    {
        if (IsInternetAvailable())
        {
            try
            {
                var res = await _server.GetAllAsync();
                return res;
            }
            catch (Exception ex)
            {
                return new List<OptionDto>();
            }
        }
        else
        {
            return new List<OptionDto>();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateOptionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.UpdateAsync(id, dto);
                return res;
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
