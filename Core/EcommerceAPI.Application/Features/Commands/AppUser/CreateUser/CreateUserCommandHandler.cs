using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.User;
using MediatR;

namespace EcommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           CreateUserResponse response = await _userService.CreateUser(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                UserName = request.UserName,
                AgreeTerms = request.AgreeTerms
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
