using AutoTest.Domain.Enums;

namespace AutoTest.BLL.DTOs.Tests.Test;

public class GenerateTestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int QuestionCount { get; set; }
    public TestLevel Level { get; set; }
    public long UserId { get; set; }
}
