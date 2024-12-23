using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoTest.BLL.DTOs.Tests.Test;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Topic;

public class TopicDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description"))]
    public string? Description { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("test")]
    public TestDto Test { get; set; }
}
