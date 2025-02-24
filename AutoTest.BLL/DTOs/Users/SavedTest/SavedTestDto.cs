using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class SavedTestDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long TestId { get; set; }
    public DateTime CreatedAt { get; set; }
}
