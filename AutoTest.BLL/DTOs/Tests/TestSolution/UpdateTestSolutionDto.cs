using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.TestSolution;

public class UpdateTestSolutionDto
{
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("finished_at")]
    public DateTime FinishedAt { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
}
