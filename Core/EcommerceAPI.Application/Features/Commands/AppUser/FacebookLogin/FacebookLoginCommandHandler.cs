using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Exceptions;
using MediatR;


namespace EcommerceAPI.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken, 900);

            if (token == null)
                throw new ExternalLoginErrorException();
            return new()
            {
                Token = token
            };

         }
    }
}
