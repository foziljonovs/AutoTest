using AutoTest.Domain.Enums;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Test;

public class CreateTestDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("level")]
    public TestLevel Level { get; set; }
    [JsonPropertyName("status")]
    public TestStatus Status { get; set; }
    [JsonPropertyName("topic_ids")]
    public List<long> Topics { get; set; } = new List<long>();
}
