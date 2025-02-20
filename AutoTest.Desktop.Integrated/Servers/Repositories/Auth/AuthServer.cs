using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Auth;
using Newtonsoft.Json;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Auth;

public class AuthServer : IAuthServer
{
    public async Task<(bool result, string token)> LoginAsync(LoginDto dto, CancellationToken cancellation = default)
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthApi.LoginApi}");
            var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");
            httpRequestMessage.Content = content;
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                //save to identity
                return (result: true, token: token);
            }
            else
            {
                return (result: false, token: "");
            }
        }
        catch
        {
            return (result: false, token: "");
        }
    }

    public async Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default)
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthApi.RegisterApi}");
            var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");
            httpRequestMessage.Content = content;

            var response = await client.SendAsync(httpRequestMessage, cancellation);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Registration failed: {errorContent}");
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
