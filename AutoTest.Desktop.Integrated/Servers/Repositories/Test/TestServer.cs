using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using Newtonsoft.Json;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Test;

public class TestServer : ITestServer
{
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
}
