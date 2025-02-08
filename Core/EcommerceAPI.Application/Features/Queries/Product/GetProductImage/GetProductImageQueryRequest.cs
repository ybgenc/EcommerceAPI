using MediatR;

namespace EcommerceAPI.Application.Features.Queries.Product.GetProductImage
{
    public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }
    }
}
