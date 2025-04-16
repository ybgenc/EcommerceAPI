using MediatR;

namespace EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryRequest : IRequest<List<GetAllCustomerQueryResponse>>
    {
    }
}