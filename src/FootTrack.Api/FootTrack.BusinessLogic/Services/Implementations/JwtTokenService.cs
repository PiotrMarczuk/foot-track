using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services.Interfaces;
using FootTrack.Settings.JwtToken;
using Microsoft.IdentityModel.Tokens;

namespace FootTrack.BusinessLogic.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJwtTokenSettings _tokenSettings;

        public JwtTokenService(IJwtTokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public string GenerateToken(Id id)
        {
            var key = Encoding.ASCII
                .GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id.Value),
                }),
                Expires = DateTime.UtcNow.Add(_tokenSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature),
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler
                .CreateToken(tokenDescriptor);
            return tokenHandler
                .WriteToken(token);
        }
    }
}
