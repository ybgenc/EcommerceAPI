using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Entities
{
    public class Basket : BaseEntitiy
    {
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
        public Order? Order { get; set; }
    }
}
