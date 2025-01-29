using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Application.Repositories.ProductRepository;
using EcommerceAPI.Persistence.Repositories.CustomerRepository;
using EcommerceAPI.Persistence.Repositories.ProductRepository;
using EcommerceAPI.Persistence.Repositories.OrderRepository;
using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Persistence.Repositories.ProductImageRepository;
using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Persistence.Repositories.InvoiceRepository;
using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Persistence.Repositories.FileRepository;
using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=EcommerceDb;"));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<EcommerceAPIDbContext>();

            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IFileWriteRepository,FileWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
            services.AddScoped<IProductImageReadRepository,ProductImageReadRepository>();
            services.AddScoped<IInvoiceReadRepository,InvoceReadRepository>();
            services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();
        }
    }
}
