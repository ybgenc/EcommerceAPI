using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

            var orderNumber = new Random().Next(100000000, 999999999);
            await _orderWriteRepository.AddAsync(new()
            {
                Description = createOrder.Description,
                Address = createOrder.Address,
                TotalPrice = createOrder.TotalPrice,
                Id = Guid.Parse(createOrder.BasketId),
                OrderNumber = orderNumber.ToString(),
                isSended = false,
                AppUserId = createOrder.AppUserId
            });
        }

        public async Task<List<Order>> GetCustomerOrders(string customerId)
        {
            var customer = await _orderReadRepository.Table.Where(c => c.AppUserId == customerId).FirstOrDefaultAsync();

            if (customer != null)
            {
                var order = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .Where(o => o.AppUserId == customerId)
                .ToListAsync();
                return order;
            }
            return new();
        }

        public async Task<List<Order>> GetOrderDetailsAsync()
        {
            var order = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .ThenInclude(p => p.ProductImageFiles)
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

        public async Task SendOrder(string orderId)
        {
            Order order = await _orderReadRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                order.isSended = true;
            }
        }

        public async Task<Order> OrderMailDetailById(string orderId)
        {
            var order = await _orderReadRepository.Table.
                Include(o=>o.AppUser)
                .Include(o => o.Basket)
                .ThenInclude(b=> b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .Where(o => o.Id == Guid.Parse( orderId))
                .FirstOrDefaultAsync();
            return order;
        }
    }
}
