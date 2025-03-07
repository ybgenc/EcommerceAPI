using MediatR;

namespace EcommerceAPI.Application.Features.Commands.AppUser.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string? userId { get; set; }
        public string? resetToken { get; set; }
        public string? newPassword { get; set; }
    }
}