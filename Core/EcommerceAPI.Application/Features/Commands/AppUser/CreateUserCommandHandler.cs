using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appUser = EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Application.Features.Commands.AppUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<appUser.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<appUser.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result =  await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.FullName,
            }, request.Password);

            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "User created successfully.";
            else
                foreach(var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}";
                }
            return response;







        }
    }
}
