using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Topic;
using AutoTest.Desktop.Integrated.Servers.Repositories.Topic;
using System.Net.NetworkInformation;

namespace AutoTest.Desktop.Integrated.Services.Topic;

public class TopicService : ITopicService
{
    private readonly ITopicServer _server;
    public TopicService()
    {
        this._server = new TopicServer();
    }
    public async Task<List<TopicDto>> GetAllAsync()
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetAllAsync();
            else
                return new List<TopicDto>();
        }
        catch(Exception ex)
        {
            return new List<TopicDto>();
        }
    }

    private bool IsInternetAvailable()
    {
        try
        {
            return true;
            //using (Ping ping = new Ping())
            //{
            //    PingReply reply = ping.Send("www.google.com");
            //    return (reply.Status == IPStatus.Success);
            //}
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
