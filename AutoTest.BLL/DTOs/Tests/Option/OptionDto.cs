using AutoTest.BLL.DTOs.Tests.Question;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    public QuestionDto Question { get; set; }
}
