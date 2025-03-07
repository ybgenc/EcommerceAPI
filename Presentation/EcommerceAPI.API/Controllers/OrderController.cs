using EcommerceAPI.Application.Features.Commands.Order.CreateOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetOrderDetail;
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
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder([FromQuery] GetOrderQueryRequest request)
        {
            List<GetOrderQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetOrderById/{OrderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetOrderDetailQueryRequest request)
        {
            List<GetOrderDetailQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
