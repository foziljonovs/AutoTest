using Newtonsoft.Json;

namespace AutoTest.BLL.DTOs.Tests.QuestionSolution;

public class UpdateQuestionSolutionDto
{
    [JsonProperty("question_id")]
    public long QuestionId { get; set; }
    [JsonProperty("test_solution_id")]
    public long TestSolutionId { get; set; }
    [JsonProperty("selected_option_id")]
    public long? SelectedOptionId { get; set; }
    [JsonProperty("custom_answer")]
    public string? CustomAnswer { get; set; }
    [JsonProperty("is_correct")]
    public bool IsCorrect { get; set; }
    [JsonProperty("score")]
    public int Score { get; set; }
}
