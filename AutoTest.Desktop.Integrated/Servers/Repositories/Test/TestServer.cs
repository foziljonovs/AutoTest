   using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Test;

public class TestServer : ITestServer
{
    public async Task<bool> AddAsync(CreateTestDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var userId = TokenHandler.ParseToken(token).Id;
            dto.UserId = userId;

            using(var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using(var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/tests"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;
                    var response = await client.SendAsync(request);

                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return true;

                    else
                        return false;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<TestDto>> CompletedTaskAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests/completed");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<TestDto> tests = JsonConvert.DeserializeObject<List<TestDto>>(response)!;

            return tests;
        }
        catch(Exception ex)
        {
            return new List<TestDto>();
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests/{id}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync(client.BaseAddress);

            if (result.IsSuccessStatusCode)
                return true;
            else 
                return false;
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
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<TestDto> tests = JsonConvert.DeserializeObject<List<TestDto>>(response)!;

            return tests;
        }
        catch(Exception ex)
        {
            return new List<TestDto>();
        }
    }

    public async Task<List<TestDto>> GetAllByTopicIdAsync(long topicId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests/{topicId}/topic");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<TestDto> tests = JsonConvert.DeserializeObject<List<TestDto>>(response)!;

            return tests;
        }
        catch(Exception ex)
        {
            return new List<TestDto>();
        }
    }

    public async Task<List<TestDto>> GetAllByUserIdAsync(long userId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests/{userId}/user");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<TestDto> tests = JsonConvert.DeserializeObject<List<TestDto>>(response)!;

            return tests;
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
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/tests/{id}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();
            TestDto test = JsonConvert.DeserializeObject<TestDto>(response)!;
            return test;
        }
        catch (Exception ex)
        {
            return new TestDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            var url = $"{AuthApi.BASE_URL}/api/tests/{id}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var json = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            request.Content = json;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
