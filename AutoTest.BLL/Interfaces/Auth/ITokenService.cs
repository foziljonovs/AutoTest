using AutoTest.Domain.Entities.Users;

namespace AutoTest.BLL.Interfaces.Auth;

public interface ITokenService
{
    string GenerateToken(User user);
}
