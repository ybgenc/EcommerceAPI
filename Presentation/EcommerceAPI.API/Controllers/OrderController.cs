using EcommerceAPI.Application.Features.Commands.Order.CreateOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder([FromQuery] GetOrderQueryRequest request)
        {
            GetOrderQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
