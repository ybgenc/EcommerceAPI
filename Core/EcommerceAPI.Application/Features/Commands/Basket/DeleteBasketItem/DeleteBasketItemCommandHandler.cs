using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, DeleteBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public DeleteBasketItemCommandHandler(IBasketItemWriteRepository basketItemWriteRepository, IBasketService basketService)
        {
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketService = basketService;
        }

        public async Task<DeleteBasketItemCommandResponse> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.DeleteBasketItemAsync(request.BasketItemId);
            await _basketItemWriteRepository.SaveAsync();
            return new();
        }
    }
}
