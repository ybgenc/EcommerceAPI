using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tokens = EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens.Token CreateAccesstoken(int second, AppUser appUser)
        {
            Tokens.Token token = new Tokens.Token();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SignInKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpireDate = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.ExpireDate,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appUser.UserName) }
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = RefreshToken();

            return token;
        }


        public string RefreshToken()
        {
            var randomBytes = new byte[32];
            using (var generate = RandomNumberGenerator.Create())
            {
                generate.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}

