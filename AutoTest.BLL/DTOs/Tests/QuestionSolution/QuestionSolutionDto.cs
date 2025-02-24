using Et = AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;
namespace AutoTest.BLL.DTOs.Tests.QuestionSolution;

public class QuestionSolutionDto
{
    public long Id { get; set; }
    public long QuestionId { get; set; }
    public Et.Question Question { get; set; }
    public long TestSolutionId { get; set; }
    public Et.TestSolution TestSolution { get; set; }
    public long? SelectedOptionId { get; set; }
    public string? CustomAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
}
