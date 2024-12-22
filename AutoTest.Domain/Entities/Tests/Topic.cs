using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Tests;

public class Topic : BaseEntity
{
    [Column("name"), MaxLength(100)]
    public required string Name { get; set; }

    [Column("description"), MaxLength(500)] 
    public string? Description { get; set; }
    [Column("test_id")]
    public required long TestId { get; set; }
    public Test Test { get; set; }
}
