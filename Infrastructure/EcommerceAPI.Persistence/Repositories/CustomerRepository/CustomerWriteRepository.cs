using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Application.Repositories.CustomerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.CustomerRepository
{
    public class CustomerWriteRepository : WriteRepository<Customer> , ICustomerWriteRepository
    {
    }
}
