using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Exceptions;
using MediatR;

namespace EcommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.LoginAsync(request.UsernameOrEmail,request.Password,900);
            if (token == null)
                throw new UserNotFoundException();
            else
                return new()
                {
                    Token = token
                };
 
        }

    }
}
