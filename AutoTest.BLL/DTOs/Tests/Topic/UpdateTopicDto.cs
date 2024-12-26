using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Topic;

public class UpdateTopicDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
}
