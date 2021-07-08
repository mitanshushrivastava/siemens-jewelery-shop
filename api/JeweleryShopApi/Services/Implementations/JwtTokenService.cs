using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JeweleryShopApi.Common;
using JeweleryShopApi.Entities;
using JeweleryShopApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JeweleryShopApi.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly ApplicationSettings settings;

        public JwtTokenService(IOptions<ApplicationSettings> settings)
        {
            this.settings = settings.Value;            
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.settings.JwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                { 
                    new Claim(Constants.ClaimName, user.UserName),
                    new Claim(Constants.ClaimRole, ((int)user.Role).ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}