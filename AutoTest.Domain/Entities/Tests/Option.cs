using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class Option : BaseEntity
{
    [Column("text"), MaxLength(500)]
    public required string Text { get; set; }
    [Column("is_correct")]
    public bool IsCorrect { get; set; }
    [Column("question_id")]
    public required long QuestionId { get; set; }
    public Question Question { get; set; }
}
