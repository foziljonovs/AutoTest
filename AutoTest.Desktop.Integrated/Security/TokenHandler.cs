﻿using System.IdentityModel.Tokens.Jwt;

namespace AutoTest.Desktop.Integrated.Security;

public static class TokenHandler
{
    public static IdentitySingelton ParseToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenInfo = tokenHandler.ReadJwtToken(token);

        var identity = new IdentitySingelton
        {
            Token = token
        };

        foreach (var claim in tokenInfo.Claims)
        {
            switch (claim.Type)
            {
                case "Id":
                    identity.Id = long.Parse(claim.Value);
                    break;
                case "Firstname":
                    identity.Firstname = claim.Value;
                    break;
                case "Lastname":
                    identity.Lastname = claim.Value;
                    break;
                case "PhoneNumber":
                    identity.PhoneNumber = claim.Value;
                    break;
                case "Role":
                    identity.RoleName = claim.Value;
                    break;
            }
        }

        return identity;
    }
}
