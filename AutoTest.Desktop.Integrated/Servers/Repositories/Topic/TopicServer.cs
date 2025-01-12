using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Topic;
using Newtonsoft.Json;
using System.Text;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Topic;

public class TopicServer : ITopicServer
{
    public async Task<bool> AddAsync(CreateTopicDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            
            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using(var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/topics"))
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

    public async Task<List<TopicDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/topics");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            var response = await message.Content.ReadAsStringAsync();

            List<TopicDto> topics = JsonConvert.DeserializeObject<List<TopicDto>>(response)!;

            return topics;
        }
        catch (Exception ex)
        {
            return new List<TopicDto>();
        }
    }
}
