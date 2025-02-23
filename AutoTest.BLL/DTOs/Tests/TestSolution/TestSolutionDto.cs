using Et = AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.DTOs.Tests.TestSolution;

public class TestSolutionDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("test")]
    public Et.Test Test { get; set; }
    [JsonPropertyName("started_at")]
    public DateTime StartedAt { get; set; }
    [JsonPropertyName("finished_at")]
    public DateTime FinishedAt { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
    public List<Et.QuestionSolution> QuestionSolutions { get; set; }
    public List<UserTestSolution> UserTestSolutions { get; set; }
}
