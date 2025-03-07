using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
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
        readonly IMailService _mailService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderWriteRepository orderWriteRepository, IMailService mailService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderWriteRepository = orderWriteRepository;
            _mailService = mailService;
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

            var orderMail = await _orderService.OrderMailDetail();

            
            await _mailService.SendOrderMailAsync(orderMail);
            return new();
        }
    }
}
