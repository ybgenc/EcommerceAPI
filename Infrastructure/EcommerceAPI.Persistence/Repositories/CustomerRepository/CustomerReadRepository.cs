using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.CustomerRepository
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
