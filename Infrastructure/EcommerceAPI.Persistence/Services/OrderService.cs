using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Identity;
using EcommerceAPI.Persistence.Repositories.OrderRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;

        }

        public async Task CreateOrderAsync(Create_Order_DTO createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Description = createOrder.Description,
                Address = createOrder.Address,
                TotalPrice = createOrder.TotalPrice,
                Id =Guid.Parse( createOrder.BasketId)
            });
        }


        public async Task<List<Order>> GetOrderDetailsAsync()
        {
            var order = await _orderReadRepository.Table 
                .Include(o => o.Basket) 
                .ThenInclude(b => b.BasketItems) 
                .ThenInclude(bi => bi.Product)
                .ToListAsync(); 

            return order;
        }


        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await _orderReadRepository.Table.ToListAsync();
            return orders;
        }

        public async Task<Order> OrderMailDetail()
        {
            var lastOrder = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .OrderByDescending(o => o.CreatedDate) 
                .FirstOrDefaultAsync(); 

            return lastOrder;
        }
    }
}
