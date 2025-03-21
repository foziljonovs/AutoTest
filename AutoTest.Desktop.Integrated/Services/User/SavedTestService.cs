﻿using AutoTest.BLL.DTOs.Users.SavedTest;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using AutoTest.Desktop.Integrated.Servers.Repositories.User;

namespace AutoTest.Desktop.Integrated.Services.User;

public class SavedTestService : ISavedTestService
{
    private readonly ISavedTestServer _server;
    public SavedTestService()
    {
        this._server = new SavedTestServer();
    }
    public async Task<bool> AddAsync(CreatedSavedTestDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.AddAsync(dto);
                return res;
            }
            else
                return false;
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
                var res = await _server.DeleteAsync(id);
                return res;
            }
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<SavedTestDto>> GetAllAsync()
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.GetAllAsync();
                return res;
            }
            else
                return new List<SavedTestDto>();
        }
        catch(Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<List<SavedTestDto>> GetAllByTestIdAsync(long testId)
    {
        try
        {
            if(IsInternetAvailable())
            {
                var res = await _server.GetAllByTestIdAsync(testId);
                return res;
            }
            else
                return new List<SavedTestDto>();
        }
        catch(Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<List<SavedTestDto>> GetAllByUserIdAsync(long userId)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.GetAllByUserIdAsync(userId);
                return res;
            }
            else
                return new List<SavedTestDto>();
        }
        catch (Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<SavedTestDto> GetByIdAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var res = await _server.GetByIdAsync(id);
                return res;
            }
            else
                return new SavedTestDto();
        }
        catch(Exception ex)
        {
            return new SavedTestDto();
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
