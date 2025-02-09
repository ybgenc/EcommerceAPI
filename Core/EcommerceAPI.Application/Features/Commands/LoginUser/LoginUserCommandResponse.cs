using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }


    }
}
