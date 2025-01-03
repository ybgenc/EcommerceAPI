﻿using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntitiy
    {
        private readonly EcommerceAPIDbContext _context;
        public WriteRepository(EcommerceAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => throw new NotImplementedException();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
           await Table.AddRangeAsync(model);
            return true;
        }

        public bool Delete(T model)
        {
            EntityEntry<T> entityState = Table.Remove(model);
            return entityState.State == EntityState.Deleted;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            T model = await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(Id));
            return Delete(model);
        }

        public bool DeleteRange(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public  Task<int> SaveAsync(T model)
            => _context.SaveChangesAsync();


    }
}
 