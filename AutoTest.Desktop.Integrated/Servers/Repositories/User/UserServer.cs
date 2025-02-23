using AutoTest.BLL.DTOs.Users.User;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.User;

public class UserServer : IUserServer
{
    public async Task<bool> ChangePasswordAsync(long id, UserChangePasswordDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var url = $"{AuthApi.BASE_URL}/api/users/{id}/change-password";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

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

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

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
        catch (Exception ex)
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
        catch (Exception ex)
        {
            return new UserDto();
        }
    }

    public Task<bool> UpdateAsync(long id, UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> VerifyPasswordAsync(long id, string password)
    {
        try
        {
            HttpClient client = new HttpClient();
            var url = $"{AuthApi.BASE_URL}/api/users/{id}/verify?password={password}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

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
