using AutoTest.BLL.Interfaces.Auth;
using AutoTest.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoTest.BLL.Services.Auth;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration.GetSection("Jwt");
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Firstname = " " + user.Lastname),
            new Claim("PhoneNumber", user.PhoneNumber),
            new Claim(ClaimTypes.Role, "User")
        };

        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDestrictor = new JwtSecurityToken(
            configuration["Issuer"],
            configuration["Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(double.Parse(configuration["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDestrictor);
    }
}
