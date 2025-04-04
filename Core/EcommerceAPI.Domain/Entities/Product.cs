﻿using EcommerceAPI.Domain.Entities.Common;

namespace EcommerceAPI.Domain.Entities
{
    public class Product : BaseEntitiy
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public ICollection<ProductImageFile>? ProductImageFiles { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }

       
    }
}
