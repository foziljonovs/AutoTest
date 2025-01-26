using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.Interfaces.OpenAI;

public interface IOpenAIService
{
    Task<long> CompleteAsync(GenerateTestDto dto);
    Task<string> GenerateAsync(GenerateTestDto dto);
    Task<TestDto> ConvertToTest(string content, TestDto dto, long userId);
}
