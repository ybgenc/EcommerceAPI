using EcommerceAPI.Application.Repositories.ProductRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.ProductRepository
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
