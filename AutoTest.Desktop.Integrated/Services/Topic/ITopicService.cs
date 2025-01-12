using AutoTest.BLL.DTOs.Tests.Topic;

namespace AutoTest.Desktop.Integrated.Services.Topic;

public interface ITopicService
{
    Task<List<TopicDto>> GetAllAsync();
    Task<bool> AddAsync(CreateTopicDto dto);
}
