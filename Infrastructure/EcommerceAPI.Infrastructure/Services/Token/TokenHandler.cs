using EcommerceAPI.Application.Abstraction.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public Application.DTOs.Token CreateAccesstoken(int minute)
        {
            Tokens.Token token = new Tokens.Token();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SignInKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpireDate = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(audience: _configuration["Token : Audience"],
                                                 issuer: _configuration["Token : Issuer"],
                                                 expires: token.ExpireDate,
                                                 notBefore: DateTime.UtcNow,
                                                 signingCredentials: signingCredentials
                                                 );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken =  tokenHandler.WriteToken(securityToken);
            return token;
        

        }
    }
}
