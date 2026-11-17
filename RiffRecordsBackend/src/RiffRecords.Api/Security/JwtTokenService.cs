using System.Globalization;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RiffRecords.Application.Dtos;

namespace RiffRecords.Api.Security
{
    public sealed class JwtTokenService(
        SigningCredentials signingCredentials,
        string issuer,
        string audience)
    {
        public string CreateToken(UserDto user)
        {
            var now = DateTime.UtcNow;

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                IssuedAt = now,
                Audience = audience,
                Expires = now.AddMinutes(15),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim("sub", user.Id.ToString(CultureInfo.InvariantCulture)),
                    new Claim("role", user.Role.ToString()),
                }),

            };

            return new JsonWebTokenHandler().CreateToken(descriptor);
        }

    }
}
