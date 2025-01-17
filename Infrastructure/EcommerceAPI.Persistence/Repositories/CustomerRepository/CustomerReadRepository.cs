﻿using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.CustomerRepository
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
