using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Topic;

public class CreateTopicDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
