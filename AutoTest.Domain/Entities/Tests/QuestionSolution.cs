using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class QuestionSolution : BaseEntity
{
    [Column("question_id")]
    public required long QuestionId { get; set; }
    [Column("question")]
    public Question Question { get; set; }
    [Column("test_solution_id")]
    public required long TestSolutionId { get; set; }
    [Column("test_solution")]
    public TestSolution TestSolution { get; set; }
    [Column("selected_option_id")]
    public long? SelectedOptionId { get; set; }
    [Column("custom_answer")]
    public string? CustomAnswer { get; set; }
    [Column("is_correct")]
    public required bool IsCorrect { get; set; }
    [Column("score")]
    public required int Score { get; set; }
}
