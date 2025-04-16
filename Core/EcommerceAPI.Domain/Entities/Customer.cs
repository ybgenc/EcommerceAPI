using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Domain.Entities
{
    public class Customer : BaseEntitiy
    {
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
