using AutoTest.BLL.DTOs.Tests.Topic;

namespace AutoTest.BLL.Interfaces.Tests.Topic;

public interface ITopicService
{
    Task<IEnumerable<TopicDto>> GetAllAsync(CancellationToken cancellation = default);
    Task<TopicDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<bool> AddAsync(CreateTopicDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateTopicDto dto, CancellationToken cancellation = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
}
