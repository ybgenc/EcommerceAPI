using EcommerceAPI.Application.DTOs.Customers;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface ICustomerService
    {
         Task DeleteCustomer(string customerId);
         Task<List<Customer>> GetAllCustomers();
         Task<List<Customer>> GetCustomerDetail(string customerId);
    }
}
