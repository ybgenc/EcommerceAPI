using EcommerceAPI.Application.Attributes.Custom;
using EcommerceAPI.Application.Consts;
using EcommerceAPI.Application.Enums;
using EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCustomer")]
        [AuthorizeDefinition(Menu =AuthorizeDefinitonConstants.Customer , ActionType =ActionType.Reading, Definition ="Get All Customer")]
        public async Task<IActionResult> GetAllCustomer([FromQuery]GetAllCustomerQueryRequest request)
        {
            List<GetAllCustomerQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
