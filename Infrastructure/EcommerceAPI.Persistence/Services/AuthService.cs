using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.DTOs.FacebookToken;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EcommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, System.Net.Http.IHttpClientFactory httpClientFactory, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }

        async Task<Token> ExternalLogin(AppUser user, string Email, string Name, UserLoginInfo info, int tokenLifeTime)
        {
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = Email,
                        UserName = Email,
                        NameSurname = Name,
                    };
                    await _userManager.CreateAsync(user);
                }

            }
            if (user != null)
            {
                await _userManager.AddLoginAsync(user, info);
                Token token = _tokenHandler.CreateAccesstoken(tokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.ExpireDate, 600 );
                return token;
            }
            else
                throw new ExternalLoginErrorException();

           
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int tokenLifeTime)
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

                return await ExternalLogin(user, userInfoRes.Email, userInfoRes.Name, info, tokenLifeTime);
            }
            else
                throw new Exception("Facebook authentication failed");
        }


        public async Task<Token> GoogleLoginAsync(string IdToken, int tokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["GoogleLogin:Audience"], }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, settings);
            var info = new UserLoginInfo(payload.Issuer, payload.Subject, payload.Issuer);

            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await ExternalLogin(user, payload.Email, payload.Name, info, tokenLifeTime);
         
        }

        public async Task<Token> LoginAsync(string UsernameOrEmail, string Password, int tokenLifeTime)
        {

            AppUser user = await _userManager.FindByNameAsync(UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(UsernameOrEmail);
                if (user == null)
                    throw new UserNotFoundException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccesstoken(tokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.ExpireDate, 10);
                return token;

            }
            else
                throw new AuthenticationErrorException();

        }
        public async Task<Token> RefreshTokenLogin(string RefreshToken , int refreshTokenLifeTime)
        {
           AppUser? user = await  _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == RefreshToken);

            if (user != null && user.RefreshTokenExpireDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccesstoken(15,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.ExpireDate, refreshTokenLifeTime);
                return token;
            }
            else
                throw new UserNotFoundException();
        }

        public async Task ResetPasswordAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
               string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
               
                byte[] resetTokenBytes = Encoding.UTF8.GetBytes(resetToken);
                
                resetToken = WebEncoders.Base64UrlEncode(resetTokenBytes);
                await _mailService.SendPasswordResetEmailAsync(email, user.Id, resetToken);

            }
        }

    }
}
