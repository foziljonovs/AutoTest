using AutoTest.Domain.Enums;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Question;

public class UpdateQuestionDto
{
    [JsonPropertyName("problem")]
    public string Problem { get; set; }
    [JsonPropertyName("type")]
    public QuestionType Type { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
}
