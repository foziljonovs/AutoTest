using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.Interfaces.OpenAI;

public interface IOpenAIService
{
    Task<long> CompleteAsync(GenerateTestDto dto);
    Task<TestDto> GenerateAsync(TestDto dto, long userId, int count);
}
