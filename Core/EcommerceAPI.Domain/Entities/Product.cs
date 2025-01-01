﻿using EcommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Entities
{
    public class Product : BaseEntitiy
    {
        public string Name { get; set; }
        public string Stock { get; set; }
        public long Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}