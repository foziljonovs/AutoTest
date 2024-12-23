using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.User;

public class LoginDto
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
