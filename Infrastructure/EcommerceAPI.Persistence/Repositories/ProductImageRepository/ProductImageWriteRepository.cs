using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.ProductImageRepository
{
    public class ProductImageWriteRepository : WriteRepository<ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
