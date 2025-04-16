using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetCustomerOrders
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQueryRequest, List<GetCustomerOrdersQueryResponse>>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;

        public GetCustomerOrdersQueryHandler(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<List<GetCustomerOrdersQueryResponse>> Handle(GetCustomerOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetCustomerOrders(request.CustomerId);

            var response = orders.Select(order => new GetCustomerOrdersQueryResponse
            {
                Address = order.Address,
                Description = order.Description,
                OrderNumber = order.OrderNumber,
                isSended = order.isSended,

                Name = order.Basket.BasketItems.Select(bi => bi.Product.Name).FirstOrDefault(),
                Price = order.Basket.BasketItems.Select(bi => bi.Product.Price).FirstOrDefault(),
                Quantity = order.Basket.BasketItems.Select(bi => bi.Quantity).FirstOrDefault(),

                TotalPrice = order.TotalPrice
            }).ToList(); 

            return response;
        }



    }
}
