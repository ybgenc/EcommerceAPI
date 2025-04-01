using EcommerceAPI.Application.Attributes.Custom;
using EcommerceAPI.Application.Consts;
using EcommerceAPI.Application.Enums;
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
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Menu = AuthorizeDefinitonConstants.Basket, Definition = "Get All Basket Item")]
        public async Task<IActionResult> GetAllBasketItem([FromQuery] GetAllBasketItemQueryRequest request)
        {
            List<GetAllBasketItemQueryResponse> response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpPost("AddItemToBasket")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Menu = AuthorizeDefinitonConstants.Basket, Definition = "Add Item To Basket")]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddBasketItemCommandRequest request)
        {
            AddBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("UpdateItemQuantity")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Menu = AuthorizeDefinitonConstants.Basket, Definition = "Update Item Quantity")]

        public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateBasketItemCommandRequest request)
        {
            UpdateBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Menu = AuthorizeDefinitonConstants.Basket, Definition = "Delete Basket Item")]
        public async Task<IActionResult> DeleteBasketItem([FromRoute] DeleteBasketItemCommandRequest request)
        {
            DeleteBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
