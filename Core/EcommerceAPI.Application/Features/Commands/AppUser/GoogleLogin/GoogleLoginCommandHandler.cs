using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using appUser = EcommerceAPI.Domain.Entities.Identity;
namespace EcommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {

        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.GoogleLoginAsync(request.IdToken, 900);

            if (token == null)
                throw new ExternalLoginErrorException();
            else
                return new()
                {
                    Token = token
                };
            


        }
    }
}
