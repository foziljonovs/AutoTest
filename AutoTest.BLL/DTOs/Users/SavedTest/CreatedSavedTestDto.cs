using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class CreatedSavedTestDto
{
    public long UserId { get; set; }
    public long TestId { get; set; }
}
