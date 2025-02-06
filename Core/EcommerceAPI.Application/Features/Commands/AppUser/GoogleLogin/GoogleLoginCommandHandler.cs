using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Exceptions;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appUser = EcommerceAPI.Domain.Entities.Identity;
namespace EcommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<appUser.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;

        public GoogleLoginCommandHandler(UserManager<appUser.AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["GoogleLogin:Audience"], }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,settings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            appUser.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if(user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                    };
                    var newUser = await _userManager.CreateAsync(user);
                    result = newUser.Succeeded;
                }

            }
            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new ExternalLoginErrorException();

            Token token = _tokenHandler.CreateAccesstoken(5);

            return new()
            {
                Token = token,
            };


        }
    }
}
