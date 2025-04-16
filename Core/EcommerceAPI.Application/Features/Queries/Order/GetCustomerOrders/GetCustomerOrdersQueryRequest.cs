using MediatR;

namespace EcommerceAPI.Application.Features.Queries.Order.GetCustomerOrders
{
    public class GetCustomerOrdersQueryRequest : IRequest<List<GetCustomerOrdersQueryResponse>>
    {
        public string CustomerId { get; set; }
    }
}