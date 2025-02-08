﻿using MediatR;

namespace EcommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AgreeTerms { get; set; }
    }
}
