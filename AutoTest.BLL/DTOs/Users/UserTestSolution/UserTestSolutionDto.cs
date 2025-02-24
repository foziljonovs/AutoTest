using AutoTest.Domain.Entities.Tests;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.UserTestSolution;

public class UserTestSolutionDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long TestSolutionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
