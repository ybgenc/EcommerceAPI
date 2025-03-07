using EcommerceAPI.Domain.Entities.Common;

namespace EcommerceAPI.Domain.Entities
{
    public class Order : BaseEntitiy
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public float TotalPrice { get; set; }
        public Basket Basket { get; set; }
    }
}
