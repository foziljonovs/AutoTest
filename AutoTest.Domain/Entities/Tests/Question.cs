using AutoTest.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class Question : BaseEntity
{
    [Column("problem")]
    public required string Problem { get; set; }
    [Column("type")]
    public QuestionType Type { get; set; }
    [Column("test_id")]
    public required long TestId { get; set; }
    public Test Test { get; set; }
    public List<Option> Options { get; set; } = new List<Option>();
    public List<QuestionSolution> QuestionSolutions { get; set; } = new List<QuestionSolution>();
}
