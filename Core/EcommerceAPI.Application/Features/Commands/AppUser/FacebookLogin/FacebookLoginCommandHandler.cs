using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.DTOs.FacebookToken;
using EcommerceAPI.Application.Exceptions;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using appUser = EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly UserManager<appUser.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;

        public FacebookLoginCommandHandler(UserManager<appUser.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string clientId = _configuration["FacebookLogin:ClientId"];
            string clientSecret = _configuration["FacebookLogin:ClientSecret"];


            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials");

            FacebookToken facebookToken = JsonSerializer.Deserialize<FacebookToken>(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookToken.AccessToken}");

            FacebookAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookAccessTokenValidation>(userAccessTokenValidation);


            if (validation.Data.IsValid)
            {
                string userInfo = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

                FacebookUserInfoResponse userInfoRes = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfo);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

                appUser.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


                
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfoRes.Email);
                    if(user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfoRes.Email,
                            UserName = userInfoRes.Email,
                            NameSurname = userInfoRes.Name
                        };
                        await _userManager.CreateAsync(user);
                    }

                }
                if (user != null)
                    await _userManager.AddLoginAsync(user, info);
                else
                    throw new ExternalLoginErrorException();

                Token token = _tokenHandler.CreateAccesstoken(5);

                return new()
                {
                    Token = token
                };

            }
            else
                throw new Exception("Facebook authentication failed");


        }
    }
}
