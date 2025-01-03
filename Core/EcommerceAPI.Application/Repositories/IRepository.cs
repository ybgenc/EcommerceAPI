﻿using EcommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntitiy
    {
         DbSet<T> Table { get; } 
    }
}
