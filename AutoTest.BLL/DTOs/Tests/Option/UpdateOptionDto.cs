using AutoTest.BLL.DTOs.Tests.Question;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Option;

public class UpdateOptionDto
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    [JsonPropertyName("is_correct")]
    public bool IsCorrect { get; set; }
    [JsonPropertyName("question_id")]
    public long QuestionId { get; set; }
}
