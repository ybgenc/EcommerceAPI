using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderWriteRepository _orderWriteRepository;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderWriteRepository orderWriteRepository)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {

            await _orderService.CreateOrderAsync(new()
            {
                Description = request.Description,
                Address = request.Address,
                TotalPrice = request.TotalPrice,
                BasketId = _basketService?.GetBasketId?.Id.ToString()
            });
            await _orderWriteRepository.SaveAsync();
            return new();
        }
    }
}
