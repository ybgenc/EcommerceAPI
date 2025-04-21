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
        readonly IMailService _mailService;

        public SendOrderCommandHandler(IOrderService orderService, IOrderWriteRepository orderWriteRepository, IMailService mailService)
        {
            _orderService = orderService;
            _orderWriteRepository = orderWriteRepository;
            _mailService = mailService;
        }

        public async Task<SendOrderCommandResponse> Handle(SendOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.SendOrder(request.OrderId);            
            var orderMail = await _orderService.OrderMailDetailById(request.OrderId);
            _mailService.OrderShippedMailAsync(orderMail);
            await _orderWriteRepository.SaveAsync();
            return new();
        }
    }
}
