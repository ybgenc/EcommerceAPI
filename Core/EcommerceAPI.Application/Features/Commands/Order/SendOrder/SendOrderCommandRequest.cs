using MediatR;

namespace EcommerceAPI.Application.Features.Commands.Order.SendOrder
{
    public class SendOrderCommandRequest : IRequest<SendOrderCommandResponse>
    {
        public string OrderId { get; set; }
    }
}