using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using Newtonsoft.Json;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.User;

public class UserServer : IUserServer
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/users");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();
            List<UserDto> users = JsonConvert.DeserializeObject<List<UserDto>>(response)!;

            return users;
        }
        catch(Exception ex)
        {
            return new List<UserDto>();
        }
    }

    public async Task<UserDto> GetByIdAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/users/{id}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();
            UserDto user = JsonConvert.DeserializeObject<UserDto>(response)!;

            return user;
        }
        catch(Exception ex)
        {
            return new UserDto();
        }
    }
}
