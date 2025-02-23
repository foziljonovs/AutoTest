using AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.UserTestSolution;

public class UserTestSolutionDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("test_solution_id")]
    public long TestSolutionId { get; set; }
    [JsonPropertyName("test_solution")]
    public TestSolution TestSolution { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
