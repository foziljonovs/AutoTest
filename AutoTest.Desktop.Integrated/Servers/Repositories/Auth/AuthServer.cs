using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Auth;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Auth;

public class AuthServer : IAuthServer
{
    public async Task<(bool result, string token)> LoginAsync(LoginDto dto, CancellationToken cancellation = default)
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthApi.LoginApi}");
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            httpRequestMessage.Content = content;
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent)!;
                string token = jsonResponse.token.ToString();

                return (result: true, token: token);
            }
            else
            {
                return (result: false, token: "");
            }
        }
        catch (Exception ex)
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
