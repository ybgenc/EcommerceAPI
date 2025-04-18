﻿using EcommerceAPI.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public ICollection<Basket>? Baskets { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public Customer Customer { get; set; }
    }
}