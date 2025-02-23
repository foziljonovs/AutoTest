using Et = AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;
namespace AutoTest.BLL.DTOs.Tests.QuestionSolution;

public class QuestionSolutionDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("question_id")]
    public long QuestionId { get; set; }
    [JsonPropertyName("question")]
    public Et.Question Question { get; set; }
    [JsonPropertyName("test_solution_id")]
    public long TestSolutionId { get; set; }
    [JsonPropertyName("test_solution")]
    public Et.TestSolution TestSolution { get; set; }
    [JsonPropertyName("selected_option_id")]
    public long? SelectedOptionId { get; set; }
    [JsonPropertyName("custom_answer")]
    public string? CustomAnswer { get; set; }
    [JsonPropertyName("is_correct")]
    public bool IsCorrect { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
