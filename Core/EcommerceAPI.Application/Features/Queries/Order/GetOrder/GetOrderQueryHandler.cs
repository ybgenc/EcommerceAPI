using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQueryRequest, GetOrderQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetOrderQueryResponse> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersAsync();

            var response = orders.SelectMany(order => order.Basket.BasketItems)
                .Select(basketItem => new
                {
                    OrderId = basketItem?.Basket?.Order?.Id,  
                    OrderDate = basketItem?.Basket?.Order.CreatedDate,
                    Name = basketItem?.Product?.Name,
                    Price = basketItem?.Product?.Price ,  
                    Quantity = basketItem?.Quantity,      
                    Description = basketItem?.Basket?.Order?.Description,
                    Address = basketItem?.Basket?.Order?.Address
                })
                .GroupBy(order => order.OrderId)  
                .ToList();

            var orderGroup = response.Select(group => new
            {
                OrderId = group.Key,
                Products = group.ToList()  
            }).ToList();

            return new()
            {
                Order = orderGroup
            };
        }





    }
}
