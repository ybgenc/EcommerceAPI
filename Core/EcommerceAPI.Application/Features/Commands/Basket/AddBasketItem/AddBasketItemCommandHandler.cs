using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommandRequest, AddBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public AddBasketItemCommandHandler(IBasketService basketService, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _basketService = basketService;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        public async Task<AddBasketItemCommandResponse> Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.AddItemToBasketAsync(new() { ProductId = request.ProductId, Quantity = request.Quantity });
            await _basketItemWriteRepository.SaveAsync();
            return new();
        }
    }
}
