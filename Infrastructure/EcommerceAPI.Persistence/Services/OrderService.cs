using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Repositories.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IBasketService _basketService;

        public OrderService(IOrderWriteRepository orderWriteRepository, IBasketService basketService)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(Create_Order_DTO createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Description = createOrder.Description,
                Address = createOrder.Address,
                Id =Guid.Parse( createOrder.BasketId)
            });
        }
    }
}
