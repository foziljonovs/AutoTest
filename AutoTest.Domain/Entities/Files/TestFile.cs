using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Files;

public class TestFile : BaseEntity
{
    [Column("file_name")]
    public required string FileName { get; set; }
    [Column("file_path")]
    public required string FilePath { get; set; }
    [Column("file_type")]
    public FileType Type { get; set; }
    [Column("test_id")]
    public required long TestId { get; set; }
    public Test Test { get; set; }
}
