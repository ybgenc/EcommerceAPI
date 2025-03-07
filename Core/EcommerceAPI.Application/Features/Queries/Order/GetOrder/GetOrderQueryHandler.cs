using EcommerceAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetOrderDetail
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQueryRequest, List<GetOrderQueryResponse>>
    {
        readonly IOrderService _orderService;

        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetOrderQueryResponse>> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersAsync();

            var orderList = orders.Select(order => new GetOrderQueryResponse
            {
                OrderDate = order.CreatedDate,
                OrderId = order.Id.ToString(),
                TotalPrice = order.TotalPrice
            }).ToList();

            var groupedOrders = orderList
                .GroupBy(g => g.OrderId)
                .Select(group => new GetOrderQueryResponse
                {
                    OrderId = group.Key, 
                    OrderDate = group.First().OrderDate,
                    TotalPrice = group.First().TotalPrice
                })
                .ToList();

            return groupedOrders;
        }

    }
}
