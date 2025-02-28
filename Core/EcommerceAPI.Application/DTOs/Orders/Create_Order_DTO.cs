using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Orders
{
    public class Create_Order_DTO
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string BasketId { get; set; }
    }
}
