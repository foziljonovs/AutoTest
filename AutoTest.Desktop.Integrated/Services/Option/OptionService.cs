﻿using AutoTest.BLL.DTOs.Tests.Option;
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
            if(IsInternetAvailable())
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
