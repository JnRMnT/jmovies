using JMovies.Entities.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JMovies.Web.Providers
{
    public class JWTTokenProvider : ITokenProvider
    {
        private byte[] signKey, encryptionKey;
        public JWTTokenProvider(byte[] signKey, byte[] encryptionKey)
        {
            this.signKey = signKey;
            this.encryptionKey = encryptionKey;
        }

        public string IssueToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Audience = "jmovier",
                Issuer = "jmovies",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signKey), SecurityAlgorithms.HmacSha512),
                EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512),
            };
            return tokenHandler.CreateEncodedJwt(tokenDescriptor);
        }
    }
}
