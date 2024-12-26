using AutoTest.Domain.Entities.Files;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using AutoTest.BLL.DTOs.User;
using System.Text.Json.Serialization;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Topic;

namespace AutoTest.BLL.DTOs.Tests.Test;

public class TestDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("level")]
    public TestLevel Level { get; set; }
    [JsonPropertyName("status")]
    public TestStatus Status { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("user")]
    public UserDto User { get; set; }
    [JsonPropertyName("questions")]
    public List<QuestionDto> Question { get; set; }
    [JsonPropertyName("topics")]
    public List<TopicDto> Topics { get; set; }
    [JsonPropertyName("files")]
    public List<TestFile> Files { get; set; } 
}
