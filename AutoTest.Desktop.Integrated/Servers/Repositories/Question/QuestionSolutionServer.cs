using AutoTest.BLL.DTOs.Tests.QuestionSolution;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Question;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Question;

public class QuestionSolutionServer : IQuestionSolutionServer
{
    public async Task<bool> AddAsync(CreateQuestionSolutionDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var url = $"{AuthApi.BASE_URL}/api/question/solutions";

            using(var client = new HttpClient() )
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using(var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.DeserializeObject(dto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;
                    var response = await client.SendAsync(request);

                    var result = await response.Content.ReadAsStringAsync();
                    if(response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/question/solutions{id}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync(client.BaseAddress);

            if(result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<QuestionSolutionDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/question/solutions");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<QuestionSolutionDto> questionSolutions = JsonConvert.DeserializeObject<List<QuestionSolutionDto>>(response)!;
            return questionSolutions;
        }
        catch(Exception ex)
        {
            return new List<QuestionSolutionDto>();
        }
    }

    public async Task<List<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/question/solutions/{id}/test-solution");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await client.GetAsync(client.BaseAddress);

            List<QuestionSolutionDto> questionSolutions = JsonConvert.DeserializeObject<List<QuestionSolutionDto>>(response)!;
            return questionSolutions;
        }
        catch(Exception ex)
        {
            return new List<QuestionSolutionDto>();
        }
    }

    public async Task<QuestionSolutionDto> GetByIdAsync(long id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/question/solutions/{id}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await client.GetAsync(client.BaseAddress);

            var questionSolution = JsonConvert.DeserializeObject<QuestionSolutionDto>(response)!;
            return questionSolution;
        }
        catch(Exception ex)
        {
            return new List<QuestionSolutionDto>();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            var url = $"{AuthApi.BASE_URL}/api/question/solutions/{id}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var json = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            request.Content = json;

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
