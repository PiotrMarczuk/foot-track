using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dawn;

using FootTrack.Api.Models;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.Settings.JwtToken;

namespace FootTrack.Api.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJwtTokenSettings _tokenSettings;
        private readonly DateTime _tokenExpirationDate;

        public JwtTokenService(IJwtTokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
            _tokenExpirationDate = DateTime.UtcNow.AddDays(7);
        }

        public string GenerateToken(User user)
        {
            Guard.Argument(user, nameof(user)).NotNull();

            var key = Encoding.ASCII
                .GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = _tokenExpirationDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler
                .CreateToken(tokenDescriptor);
            return tokenHandler
                .WriteToken(token);
        }
    }
}
