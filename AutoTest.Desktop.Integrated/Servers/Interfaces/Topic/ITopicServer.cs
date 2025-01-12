using AutoTest.BLL.DTOs.Tests.Topic;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Topic;

public interface ITopicServer
{
    Task<List<TopicDto>> GetAllAsync();
    Task<bool> AddAsync(CreateTopicDto dto);
}
