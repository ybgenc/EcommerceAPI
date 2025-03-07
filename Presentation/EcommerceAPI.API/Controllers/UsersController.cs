using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using EcommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;
using EcommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using EcommerceAPI.Application.Features.Commands.AppUser.PasswordReset;
using EcommerceAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mail;

        public UsersController(IMediator mediator, IMailService mail)
        {
            _mediator = mediator;
            _mail = mail;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response =  await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(PasswordResetCommandRequest request)
        {
            PasswordResetCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
