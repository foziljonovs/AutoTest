using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.TestSolution;

public class UpdateTestSolutionDto
{
    public long TestId { get; set; }
    public int Score { get; set; }
}
