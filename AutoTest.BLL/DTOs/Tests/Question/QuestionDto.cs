using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Question;

public class QuestionDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("problem")]
    public string Problem { get; set; }
    [JsonPropertyName("type")]
    public QuestionType Type { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("test")]
    public TestDto Test { get; set; }
    [JsonPropertyName("options")]
    public List<Option> Options { get; set; } = new List<Option>(); //OptionDto
}
