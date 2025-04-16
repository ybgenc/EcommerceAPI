using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Domain.Entities
{
    public class Order : BaseEntitiy
    {
        public string? Description { get; set; }
        public string? Address { get; set; }
        public float TotalPrice { get; set; }
        public Basket? Basket { get; set; }
        public bool isSended { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string OrderNumber { get; set; }

    }
}
