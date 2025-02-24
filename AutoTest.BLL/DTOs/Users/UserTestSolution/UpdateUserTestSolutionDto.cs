using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.UserTestSolution;

public class UpdateUserTestSolutionDto
{
    public long UserId { get; set; }
    public long TestSolutionId { get; set; }
}
