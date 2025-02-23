using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.UserTestSolution;

public class CreateUserTestSolutionDto
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("test_solution_id")]
    public long TestSolutionId { get; set; }
}
