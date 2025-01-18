using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Question;
using Newtonsoft.Json;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Question;

public class QuestionServer : IQuestionServer
{
    public Task<bool> AddAsync(CreateQuestionDto dto)
    {
        throw new NotImplementedException();
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
