﻿using EcommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Entities
{
    public class BasketItem : BaseEntitiy
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public Basket? Basket { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
