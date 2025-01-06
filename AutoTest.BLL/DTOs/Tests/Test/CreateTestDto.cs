using Et = AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Test;

public class CreateTestDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("level")]
    public TestLevel Level { get; set; }
    [JsonPropertyName("status")]
    public TestStatus Status { get; set; }
    public List<Et.Topic> Topics { get; set; } = new List<Et.Topic>();
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}
