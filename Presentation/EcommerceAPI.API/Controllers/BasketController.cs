using EcommerceAPI.Application.Features.Commands.Basket.AddBasketItem;
using EcommerceAPI.Application.Features.Commands.Basket.DeleteBasketItem;
using EcommerceAPI.Application.Features.Commands.Basket.UpdateBasketItem;
using EcommerceAPI.Application.Features.Queries.Basket.GetAllBasketItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetAllBasketItem([FromQuery] GetAllBasketItemQueryRequest request)
        {
            List<GetAllBasketItemQueryResponse> response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpPost("AddItemToBasket")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddBasketItemCommandRequest request)
        {
            AddBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("UpdateItemQuantity")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateBasketItemCommandRequest request)
        {
            UpdateBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteBasketItem([FromRoute] DeleteBasketItemCommandRequest request)
        {
            DeleteBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
