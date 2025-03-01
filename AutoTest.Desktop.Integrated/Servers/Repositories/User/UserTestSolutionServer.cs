using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using Newtonsoft.Json;
using System.Data.Common;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.User;

public class UserTestSolutionServer : IUserTestSolutionServer
{
    public async Task<long> AddAsync(CreateUserTestSolutionDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using(var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using(var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/user/test-solutions"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;
                    var response = await client.SendAsync(request);

                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return long.Parse(result);
                    else
                        return -1;
                }
            }
        }
        catch(Exception ex)
        {
            return -1;
        }
    }

    public async Task<List<UserTestSolutionDto>> GetAllByTestIdAsync(long testId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/user/test-solutions{testId}/test");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<UserTestSolutionDto> userTestSolutions = JsonConvert.DeserializeObject<List<UserTestSolutionDto>>(response)!;
            return userTestSolutions;
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
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/user/test-solutions/{userId}/user");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<UserTestSolutionDto> userTestSolutions = JsonConvert.DeserializeObject<List<UserTestSolutionDto>>(response)!;
            return userTestSolutions;
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
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            var url = $"{AuthApi.BASE_URL}/api/user/test-solutions/{id}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authozation", $"Bearer {token}");

            var json = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            request.Content = json;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
