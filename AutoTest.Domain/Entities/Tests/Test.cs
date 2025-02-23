using AutoTest.Domain.Entities.Files;
using AutoTest.Domain.Entities.Users;
using AutoTest.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class Test : BaseEntity
{
    [MaxLength(100), Column("title")]
    public required string Title { get; set; }
    [MaxLength(500), Column("description")]
    public string? Description { get; set; }
    [Column("level")]
    public TestLevel Level { get; set; }
    [Column("status")]
    public TestStatus Status { get; set; }
    [Column("user_id")]
    public required long UserId { get; set; }
    public User User { get; set; }
    public List<Question> Question { get; set; } = new List<Question>();
    public List<Topic> Topics { get; set; } = new List<Topic>();
    public List<TestFile> Files { get; set; } = new List<TestFile>();
    public List<TestSolution> TestSolutions { get; set; } = new List<TestSolution>();
    public List<SavedTest> SavedTests { get; set; } = new List<SavedTest>();
}
