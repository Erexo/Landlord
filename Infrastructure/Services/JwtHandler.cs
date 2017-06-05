using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly AuthenticationSettings _settings;

        public JwtHandler(AuthenticationSettings settings)
        {
            _settings = settings;
        }
        
        public JwtDTO CreateToken(string login)
        {
            var now = DateTime.UtcNow;

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.UniqueName, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToEpoch().ToString(), ClaimValueTypes.Integer64)
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.PrivateKey)), SecurityAlgorithms.HmacSha256);
            var expiry = now.AddMinutes(_settings.ExpiryTime);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDTO
            {
                Token = token,
                Expiry = expiry.ToEpoch()
            };

        }
    }
}
