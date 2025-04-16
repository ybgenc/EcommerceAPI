using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Customers
{
    public class Customer_With_Order_DTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
