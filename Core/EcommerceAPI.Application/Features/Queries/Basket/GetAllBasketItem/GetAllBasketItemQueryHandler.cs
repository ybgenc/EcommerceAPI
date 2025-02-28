using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using EcommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Basket.GetAllBasketItem
{
    public class GetAllBasketItemQueryHandler : IRequestHandler<GetAllBasketItemQueryRequest, List<GetAllBasketItemQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetAllBasketItemQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }


        public async Task<List<GetAllBasketItemQueryResponse>> Handle(GetAllBasketItemQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemAsync();
            var basketId =  _basketService.GetBasketId?.Id.ToString();
            return basketItems.Select(item => new GetAllBasketItemQueryResponse
            {
                BasketItemId = item.Id.ToString(),
                Name = item.Product.Name,
                Price = item.Product.Price,
                Quantity = item.Quantity,
                BasketId = basketId,
            }).ToList();


        }

    }
}
