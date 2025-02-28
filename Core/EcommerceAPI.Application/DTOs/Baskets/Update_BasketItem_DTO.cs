using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Baskets
{
    public class Update_BasketItem_DTO
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
