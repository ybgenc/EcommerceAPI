using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using appUser = EcommerceAPI.Domain.Entities.Identity;
using Tokens = EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<appUser.AppUser> _userManager;
        readonly SignInManager<appUser.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<appUser.AppUser> userManager, SignInManager<appUser.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            appUser.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Tokens.Token token = _tokenHandler.CreateAccesstoken(5);
                return new LoginUserCommandResponse()
                {
                    Token = token
                };
            }
            throw new AuthenticationErrorException();
        }

    }
}
