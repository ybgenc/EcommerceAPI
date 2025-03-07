using EcommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;
using EcommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using EcommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;
using EcommerceAPI.Application.Features.Commands.LoginUser;
using EcommerceAPI.Application.Features.Commands.Mail.PasswordResetMail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("RefreshTokenLogin")]
        public async Task<IActionResult> RefreshTokenLogin( [FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("Password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody]PasswordResetMailCommandRequest request)
        {
            PasswordResetMailCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
