using EcommerceAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> _logger;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("get all products");

            var products = await _productReadRepository.GetAll(false).Include(p => p.ProductImageFiles).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.Title,
                p.Description,
                p.CreatedDate,
                p.UpdatedDate,
                p.ProductImageFiles
            }).ToListAsync();

            return new()
            {
                Products = products
            };
        }

    }
}
