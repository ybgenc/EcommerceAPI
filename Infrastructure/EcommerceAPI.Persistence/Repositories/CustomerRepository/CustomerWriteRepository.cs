using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.CustomerRepository
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
