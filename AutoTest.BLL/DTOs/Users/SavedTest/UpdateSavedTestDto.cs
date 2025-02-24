using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class UpdateSavedTestDto
{
    public long UserId { get; set; }
    public long TestId { get; set; }
}
