using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Basket.AddBasketItem
{
    public class AddBasketItemCommandRequest : IRequest<AddBasketItemCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
