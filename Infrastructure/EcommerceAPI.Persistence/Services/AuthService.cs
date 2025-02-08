using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.DTOs.FacebookToken;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EcommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, System.Net.Http.IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<Token> FacebookLoginAsync(string authToken)
        {

            string clientId = _configuration["FacebookLogin:ClientId"];
            string clientSecret = _configuration["FacebookLogin:ClientSecret"];


            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials");

            FacebookToken? facebookToken = JsonSerializer.Deserialize<FacebookToken>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookToken.AccessToken}");

            FacebookAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookAccessTokenValidation>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfo = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? userInfoRes = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfo);
                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

                AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);



                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfoRes?.Email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfoRes?.Email,
                            UserName = userInfoRes?.Email,
                            NameSurname = userInfoRes?.Name
                        };
                        await _userManager.CreateAsync(user);
                    }

                }
                if (user != null)
                    await _userManager.AddLoginAsync(user, info);
                else
                    throw new ExternalLoginErrorException();

                Token token = _tokenHandler.CreateAccesstoken(5);

                return token;
            }
            else
                throw new Exception("Facebook authentication failed");
        }


        public Task<Token> GoogleLoginAsync(string IdToken)
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }
    }
}
