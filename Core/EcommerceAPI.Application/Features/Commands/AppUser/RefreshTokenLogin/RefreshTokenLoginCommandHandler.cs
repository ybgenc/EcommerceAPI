using EcommerceAPI.Application.Abstraction.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using appUser = EcommerceAPI.Domain.Entities.Identity;
using System.Threading.Tasks;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.RefreshTokenLogin(request.RefreshToken);

            if (token == null)
                throw new LoginErrorException();
            else
                return new()
                {
                    Token = token
                };
        }
    }
}

