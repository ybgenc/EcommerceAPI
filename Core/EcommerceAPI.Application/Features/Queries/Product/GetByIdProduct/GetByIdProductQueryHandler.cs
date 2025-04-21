using EcommerceAPI.Application.Repositories.ProductRepository;
using EcommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest , GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            var productDetail = new
            {
                product.Id,
                product.Name,
                product.Stock,
                product.Price,
                product.Title,
                product.Description,
                product.CreatedDate,
                product.UpdatedDate,
                ProductImageFiles = product.ProductImageFiles.Select(img => new
                {
                    img.Id,
                    img.FileName,
                    img.Path,
                    img.Storage,
                    img.ShowCase,
                    img.CreatedDate,
                    img.UpdatedDate,
                     
                })
            };

            return new GetByIdProductQueryResponse
            {
                product = productDetail 
            };
        }

    }
}
