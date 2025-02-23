using AutoTest.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class TestSolution : BaseEntity
{
    [Column("test_id")]
    public required long TestId { get; set; }
    [Column("test")]
    public Test Test { get; set; }
    [Column("started_at")]
    public DateTime StartedAt { get; set; }
    [Column("finished_at")]
    public DateTime FinishedAt { get; set; }
    [Column("score")]
    public required int Score { get; set; }
    public List<QuestionSolution> QuestionSolutions { get; set; } = new List<QuestionSolution>();
    public List<UserTestSolution> UserTestSolutions { get; set; } = new List<UserTestSolution>();
}
