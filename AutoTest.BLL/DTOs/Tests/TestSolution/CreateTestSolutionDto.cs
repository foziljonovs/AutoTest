using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.TestSolution;

public class CreateTestSolutionDto
{
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("started_at")]
    public DateTime StartedAt { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
}
