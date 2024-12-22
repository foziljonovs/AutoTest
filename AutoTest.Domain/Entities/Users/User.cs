using AutoTest.Domain.Entities.Tests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Users;

public class User : BaseEntity
{
    [MaxLength(50), Column("first_name")]
    public required string Firstname { get; set; }
    [MaxLength(50), Column("last_name")]
    public required string Lastname { get; set; }
    [Column("password")]
    public required string Password { get; set; }
    [Column("phone_number")]
    public required string PhoneNumber { get; set; }
    [Column("salt")]
    public string Salt { get; set; }
    public List<Test> Tests { get; set; } = new List<Test>();
}
