using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.SavedTest;

public class SavedTestDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("test_id")]
    public long TestId { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
