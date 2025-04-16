using EcommerceAPI.Domain.Entities.Identity;
using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Order.GetOrder
{
    public class GetOrderDetailQueryResponse
    {
        public string? Description { get; set; }
        public string? Address { get; set; }
        public float? TotalPrice { get; set; }
        public bool? isSended { get; set; }
        public string? OrderNumber { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }

    }
}
