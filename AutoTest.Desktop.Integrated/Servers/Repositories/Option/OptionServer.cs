﻿using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Option;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Option;

public class OptionServer : IOptionServer
{
    public async Task<long> AddAsync(CreateOptionDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/options"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;
                    var response = await client.SendAsync(request);

                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<long>(result);
                    else
                        return -1;
                }
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public Task<List<OptionDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
