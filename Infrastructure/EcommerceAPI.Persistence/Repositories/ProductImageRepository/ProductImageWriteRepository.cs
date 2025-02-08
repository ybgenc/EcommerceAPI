using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.ProductImageRepository
{
    public class ProductImageWriteRepository : WriteRepository<ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
