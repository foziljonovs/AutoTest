using AutoTest.Domain.Entities.Tests;
using Et = AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class SavedTestDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public Et.User? User { get; set; }
    public long TestId { get; set; }
    public Test? Test { get; set; }
    public DateTime CreatedAt { get; set; }
}
