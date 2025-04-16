using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Customers;
using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer;
using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class CustomerService : ICustomerService
    {
        ICustomerReadRepository _customerReadRepository;
        ICustomerWriteRepository _customerWriteRepository;
        IOrderReadRepository _orderReadRepository;

        public CustomerService(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        public async Task DeleteCustomer(string customerId)
        {
           var customer =  await _customerReadRepository.GetByIdAsync(customerId);
            if(customer != null)
            {
                await _customerWriteRepository.DeleteAsync(customerId);
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            //      var orders = await _orderReadRepository.Table
            //.Include(o => o.AppUser)
            //.ToListAsync();

            //      var grouped = orders
            //          .Where(o => o.AppUser != null)
            //          .GroupBy(o => o.AppUser)
            //          .Select(g => new GetAllCustomerQueryResponse
            //          {
            //              Id = g.Key.Id.ToString(),
            //              Name = g.Key.UserName,
            //              Orders = g.Select(o => new Order_List_DTO
            //              {
            //                  Id = o.Id
            //              }).ToList()
            //          }).ToList();

            //      return grouped;
            return null;
        }

        public async Task<List<Customer>> GetCustomerDetail(string customerId)
        {
            return await _customerReadRepository.Table 
                .Include(c => c.User)
                .Where(c => c.Id == Guid.Parse(customerId))
                .ToListAsync();
        }
    }
}
