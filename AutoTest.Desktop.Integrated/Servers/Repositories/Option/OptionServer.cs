using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Option;
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

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/options/{id}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await client.DeleteAsync(client.BaseAddress);

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

    public async Task<List<OptionDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/options");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<OptionDto> options = JsonConvert.DeserializeObject<List<OptionDto>>(response)!;
            return options;
        }
        catch(Exception ex)
        {
            return new List<OptionDto>();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateOptionDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            var url = $"{AuthApi.BASE_URL}/api/options/{id}";
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
        catch(Exception ex)
        {
            return false;
        }
    }
}
