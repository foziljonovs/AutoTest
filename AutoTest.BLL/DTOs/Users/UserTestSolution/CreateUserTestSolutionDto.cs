using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.UserTestSolution;

public class CreateUserTestSolutionDto
{
    public long UserId { get; set; }
    public long TestSolutionId { get; set; }
}
