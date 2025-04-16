using EcommerceAPI.Application.Attributes.Custom;
using EcommerceAPI.Application.Consts;
using EcommerceAPI.Application.Enums;
using EcommerceAPI.Application.Features.Commands.Order.CreateOrder;
using EcommerceAPI.Application.Features.Commands.Order.SendOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetCustomerOrders;
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
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Order, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAllOrder")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Order, ActionType = ActionType.Reading, Definition = "Get All Order")]
        public async Task<IActionResult> GetAllOrder([FromQuery] GetOrderQueryRequest request)
        {
            List<GetOrderQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetOrderById/{OrderId}")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Order, ActionType = ActionType.Updating, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetOrderDetailQueryRequest request)
        {
            List<GetOrderDetailQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetCustomerOrder/{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrder([FromRoute] GetCustomerOrdersQueryRequest request)
        {
            List<GetCustomerOrdersQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("SendOrder")]
        public async Task<IActionResult> SendOrder(SendOrderCommandRequest request)
        {
            SendOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
