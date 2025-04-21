using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IOrderService 
    {
        Task CreateOrderAsync(Create_Order_DTO createOrder);

        Task<List<Order>> GetOrderDetailsAsync();

        Task<List<Order>> GetOrdersAsync();
        Task<Order> OrderMailDetail();
        Task<Order> OrderMailDetailById(string orderId);
        Task SendOrder(string  orderId);
        Task<List<Order>> GetCustomerOrders(string customerId);
    }
}
