using System.Text.Json.Serialization;
using Et = AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.DTOs.Tests.Option;

public class OptionDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("text")]
    public string Text { get; set; }
    [JsonPropertyName("is_correct")]
    public bool IsCorrect { get; set; }
    [JsonPropertyName("question_id")]
    public long QuestionId { get; set; }
    [JsonPropertyName("question")]
    public Et.Question Question { get; set; }
    [JsonPropertyName("is_change")]
    public bool IsChange { get; set; }
}
