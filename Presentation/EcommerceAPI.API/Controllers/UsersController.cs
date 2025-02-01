using EcommerceAPI.Application.Features.Commands.AppUser;
using EcommerceAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response =  await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login( [FromBody]LoginUserCommandRequest loginUserCommandRequest)
        {
            await _mediator.Send(loginUserCommandRequest);
            return Ok();
        }
    }
}
