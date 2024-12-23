using AutoTest.BLL.Interfaces.OpenAI;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AutoTest.BLL.Services.OpenAI;

public class OpenAIService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IOpenAIService
{
    private readonly HttpClient _httpClient;
    private const string OpenAiApiUrl = "https://api.openai.com/v1/chat/completions";
    private readonly string _apiKey = configuration["OpenAI:ApiKey"];
    public async Task<string> GenerateTestAsync(string topic, int count, string level, long userId, CancellationToken cancellation = default)
    {
        try
        {
            var promt = $@"Generate a {level} level test on the topic of {topic}.
                Include {count} multiple-choice questions.
                Format the response as JSON with fields:
                - 'Topic' (string)
                - 'Level' (string)
                - 'Questions' (array of objects with fields: 'Question', 'Options', and 'Answer').";

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = promt
                    }
                },
                max_tokens = 1000
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(OpenAiApiUrl, requestContent, cancellation);
            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                throw new Exception("Error while generating test");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error while generating test", ex);
        }
    }
}
