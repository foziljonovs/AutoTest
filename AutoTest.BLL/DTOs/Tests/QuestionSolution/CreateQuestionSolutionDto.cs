using Newtonsoft.Json;

namespace AutoTest.BLL.DTOs.Tests.QuestionSolution;

public class CreateQuestionSolutionDto
{
    public long QuestionId { get; set; }
    public long TestSolutionId { get; set; }
    public long? SelectedOptionId { get; set; }
    public string? CustomAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public int Score { get; set; }
}
