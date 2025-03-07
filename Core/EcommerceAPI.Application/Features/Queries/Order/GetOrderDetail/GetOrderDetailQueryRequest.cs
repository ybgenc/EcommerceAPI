using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetOrder
{
    public class GetOrderDetailQueryRequest : IRequest<List<GetOrderDetailQueryResponse>>
    {
        public string OrderId { get; set; } 
    }

}
