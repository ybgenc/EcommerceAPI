using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Repositories.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Order.SendOrder
{
    public class SendOrderCommandHandler : IRequestHandler<SendOrderCommandRequest, SendOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IOrderWriteRepository _orderWriteRepository;

        public SendOrderCommandHandler(IOrderService orderService, IOrderWriteRepository orderWriteRepository)
        {
            _orderService = orderService;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<SendOrderCommandResponse> Handle(SendOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.SendOrder(request.OrderId);
            await _orderWriteRepository.SaveAsync();
            return new();
        }
    }
}
