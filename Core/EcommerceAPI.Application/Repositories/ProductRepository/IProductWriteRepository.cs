﻿using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Repositories.ProductRepository
{
    public interface IProductWriteRepository : IWriteRepository<Product>
    {
    }
}
