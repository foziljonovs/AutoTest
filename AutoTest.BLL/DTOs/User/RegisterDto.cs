﻿using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.User;

public class RegisterDto
{
    [JsonPropertyName("first_name")]
    public string Firstname { get; set; }
    [JsonPropertyName("last_name")]
    public string Lastname { get; set; }
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
