using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Users.User;

public class UserChangePasswordDto
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("current_password")]
    public string CurrentPassword { get; set; }
    [JsonPropertyName("new_password")]
    public string NewPassword { get; set; }
}
