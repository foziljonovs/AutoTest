﻿using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Domain.Enums;
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
    public List<OptionDto> Options { get; set; } = new List<OptionDto>();
}
