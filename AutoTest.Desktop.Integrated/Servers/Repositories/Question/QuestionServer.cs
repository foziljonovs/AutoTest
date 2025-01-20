using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Question;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Question;

public class QuestionServer : IQuestionServer
{
    public async Task<long> AddAsync(CreateQuestionDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);

                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/questions"))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

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
        catch(Exception ex)
        {
            return -1;
        }
    }

    public async Task<List<QuestionDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/questions");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<QuestionDto> questions = JsonConvert.DeserializeObject<List<QuestionDto>>(response)!;

            return questions;
        }
        catch(Exception ex)
        {
            return new List<QuestionDto>();
        }
    }

    public async Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/questions/test/{testId}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<QuestionDto> questions = JsonConvert.DeserializeObject<List<QuestionDto>>(response)!;
            return questions;
        }
        catch(Exception ex)
        {
            return new List<QuestionDto>();
        }
    }
}
