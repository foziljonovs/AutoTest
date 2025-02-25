using AutoTest.BLL.DTOs.Users.SavedTest;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.User;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.User;

public class SavedTestServer : ISavedTestServer
{
    public async Task<bool> AddAsync(CreatedSavedTestDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var userId = TokenHandler.ParseToken(token).Id;
            dto.UserId = userId;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/saved-tests"))
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
        catch(Exception ex)
        {
            return false;
        }
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SavedTestDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/saved-tests");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<SavedTestDto> savedTests = JsonConvert.DeserializeObject<List<SavedTestDto>>(response)!;
            return savedTests;
        }
        catch(Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<List<SavedTestDto>> GetAllByTestIdAsync(long testId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/saved-tests/{testId}/test");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IdentitySingelton.GetInstance().Token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            var response = await message.Content.ReadAsStringAsync();

            List<SavedTestDto> savedTests = JsonConvert.DeserializeObject<List<SavedTestDto>>(response)!;
            return savedTests;
        }
        catch(Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<List<SavedTestDto>> GetAllByUserIdAsync(long userId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/saved-tests/{userId}/user");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IdentitySingelton.GetInstance().Token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            var response = await message.Content.ReadAsStringAsync();

            List<SavedTestDto> savedTests = JsonConvert.DeserializeObject<List<SavedTestDto>>(response)!;
            return savedTests;
        }
        catch(Exception ex)
        {
            return new List<SavedTestDto>();
        }
    }

    public async Task<SavedTestDto> GetByIdAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/saved-tests/{id}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            var response = await message.Content.ReadAsStringAsync();

            SavedTestDto savedTest = JsonConvert.DeserializeObject<SavedTestDto>(response)!;
            return savedTest;
        }
        catch(Exception ex)
        {
            return new SavedTestDto();
        }
    }
}
