using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.Interfaces.OpenAI;

public interface IOpenAIService
{
    Task<string> GenerateTestAsync(string topic, int count, string level, long userId, CancellationToken cancellation = default);
}
