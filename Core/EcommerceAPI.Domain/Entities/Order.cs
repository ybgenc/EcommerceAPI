using EcommerceAPI.Domain.Entities.Common;

namespace EcommerceAPI.Domain.Entities
{
    public class Order : BaseEntitiy
    {
        public string Description { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
