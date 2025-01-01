using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=EcommerceDb;"));
        }
    }
}
