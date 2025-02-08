using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.ProductImageRepository
{
    public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageReadRepository
    {
        public ProductImageReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
