using Et = AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.DTOs.Tests.TestSolution;

public class TestSolutionDto
{
    public long Id { get; set; }
    public long TestId { get; set; }
    public Et.Test Test { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public int Score { get; set; }
    public List<Et.QuestionSolution> QuestionSolutions { get; set; }
}
