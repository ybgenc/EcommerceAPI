using EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer([FromQuery]GetAllCustomerQueryRequest request)
        {
            List<GetAllCustomerQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
