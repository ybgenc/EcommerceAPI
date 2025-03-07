using MediatR;

namespace EcommerceAPI.Application.Features.Commands.Mail.PasswordResetMail
{
    public class PasswordResetMailCommandRequest : IRequest<PasswordResetMailCommandResponse>
    {
        public string Email { get; set; }
    }
}