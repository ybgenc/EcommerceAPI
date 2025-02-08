using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
