using EcommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
    }
    public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }

    }
    public class LoginUserCommandErrorResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }

    }
}
