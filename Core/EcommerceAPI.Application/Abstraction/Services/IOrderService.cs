using EcommerceAPI.Application.DTOs.Orders;
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
    }
}
