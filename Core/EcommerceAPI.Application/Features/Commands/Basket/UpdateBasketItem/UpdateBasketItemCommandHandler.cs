using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Basket.UpdateBasketItem
{
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommandRequest, UpdateBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public UpdateBasketItemCommandHandler(IBasketService basketService, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _basketService = basketService;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        public async Task<UpdateBasketItemCommandResponse> Handle(UpdateBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateBasketItemAsync(new() { BasketItemId = request.BasketItemId, Quantity = request.Quantity});
            await _basketItemWriteRepository.SaveAsync();
            return new();
        }
    }
}
