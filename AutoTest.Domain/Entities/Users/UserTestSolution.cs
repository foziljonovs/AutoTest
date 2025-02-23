using AutoTest.Domain.Entities.Tests;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Users;

public class UserTestSolution : BaseEntity
{
    [Column("user_id")]
    public required long UserId { get; set; }
    [Column("user")]
    public User User { get; set; }
    [Column("test_solution_id")]
    public required long TestSolutionId { get; set; }
    [Column("test_solution")]
    public TestSolution TestSolution { get; set; }
}
