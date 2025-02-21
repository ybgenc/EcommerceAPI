using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using EcommerceAPI.Application.Repositories.BasketRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.BasketRepository
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
