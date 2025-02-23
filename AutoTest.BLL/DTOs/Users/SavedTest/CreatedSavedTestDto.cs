using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class CreatedSavedTestDto
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
}
