using EcommerceAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Mail.PasswordResetMail
{
    public class PasswordResetMailCommandHandler : IRequestHandler<PasswordResetMailCommandRequest, PasswordResetMailCommandResponse>
    {
        readonly IAuthService _authService;

        public PasswordResetMailCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<PasswordResetMailCommandResponse> Handle(PasswordResetMailCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.ResetPasswordAsync(request.Email);
            return new();
        }
    }
}
