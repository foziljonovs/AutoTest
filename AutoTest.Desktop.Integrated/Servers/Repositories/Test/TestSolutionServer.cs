using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Test;

public class TestSolutionServer : ITestSolutionServer
{
    public async Task<long> AddAsync(CreateTestSolutionDto createTestSolutionDto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var url = $"{AuthApi.BASE_URL}/api/test/solutions";

            using(var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using(var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(createTestSolutionDto);
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

    public async Task<List<TestSolutionDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/test/solutions");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<TestSolutionDto> testSolutions = JsonConvert.DeserializeObject<List<TestSolutionDto>>(response)!;

            return testSolutions;
        }
        catch (Exception ex)
        {
            return new List<TestSolutionDto>();
        }
    }

    public async Task<TestSolutionDto> GetByIdAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/test/solutions/{id}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();
            TestSolutionDto testSolution = JsonConvert.DeserializeObject<TestSolutionDto>(response)!;
            
            return testSolution;
        }
        catch(Exception ex)
        {
            return new TestSolutionDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            var url = $"{AuthApi.BASE_URL}/api/test/solutions/{id}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var json = new StringContent(JsonConvert.SerializeObject(updateTestSolutionDto), Encoding.UTF8, "application/json");
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
