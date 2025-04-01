﻿namespace EcommerceAPI.Domain.Entities
{
    public class ProductImageFile : File
    {
        public bool ShowCase { get; set; }
        public ICollection<Product>? Product { get; set; }

    }
}
