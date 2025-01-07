using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Integrated.Api.Auth;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Topic;
using Newtonsoft.Json;

namespace AutoTest.Desktop.Integrated.Servers.Repositories.Topic;

public class TopicServer : ITopicServer
{
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
