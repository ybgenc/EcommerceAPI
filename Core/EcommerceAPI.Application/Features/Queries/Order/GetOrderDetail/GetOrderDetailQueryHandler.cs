using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Features.Queries.Order.GetCustomerOrders;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetOrder
{
    public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQueryRequest, List<GetOrderDetailQueryResponse>>
    {
        readonly IOrderService _orderService;
        readonly IOrderReadRepository _orderReadRepository;
        public GetOrderDetailQueryHandler(IOrderService orderService, IOrderReadRepository orderReadRepository)
        {
            _orderService = orderService;
            _orderReadRepository = orderReadRepository;
        }
        public async Task<List<GetOrderDetailQueryResponse>> Handle(GetOrderDetailQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Order GetOrder = await _orderReadRepository.GetByIdAsync(request.OrderId);
            var orders = await _orderService.GetOrderDetailsAsync();
            var orderDetail = orders.FirstOrDefault(o => o.Id == GetOrder.Id);

            return orderDetail.Basket.BasketItems
                .Select(basketItem => new GetOrderDetailQueryResponse
                {
                    Name = basketItem?.Product?.Name,
                    Price = basketItem?.Product?.Price ?? 0f,
                    Quantity = basketItem?.Quantity ?? 0,
                    Description = basketItem?.Basket?.Order?.Description,
                    Address = basketItem?.Basket?.Order?.Address,
                    isSended = basketItem.Basket.Order.isSended,
                    OrderNumber = basketItem.Basket.Order.OrderNumber,
                    TotalPrice = basketItem.Basket.Order.TotalPrice,
                    OrderDate  = basketItem.Basket.Order.CreatedDate,
                    imagePath = basketItem.Product?.ProductImageFiles?.FirstOrDefault(img => img.ShowCase == true)?.Path

                }).ToList();
        }

    }
}
