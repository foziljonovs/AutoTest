using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.User;

public class LoginDto
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
