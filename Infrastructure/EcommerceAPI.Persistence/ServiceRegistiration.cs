using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.Abstraction.Services.Authentication;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using EcommerceAPI.Application.Repositories.BasketRepository;
using EcommerceAPI.Application.Repositories.CustomerRepository;
using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Application.Repositories.ProductRepository;
using EcommerceAPI.Domain.Entities.Identity;
using EcommerceAPI.Persistence.Contexts;
using EcommerceAPI.Persistence.Repositories.BasketItemRepository;
using EcommerceAPI.Persistence.Repositories.BasketRepository;
using EcommerceAPI.Persistence.Repositories.CustomerRepository;
using EcommerceAPI.Persistence.Repositories.FileRepository;
using EcommerceAPI.Persistence.Repositories.InvoiceRepository;
using EcommerceAPI.Persistence.Repositories.OrderRepository;
using EcommerceAPI.Persistence.Repositories.ProductImageRepository;
using EcommerceAPI.Persistence.Repositories.ProductRepository;
using EcommerceAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Persistence
{
    public static class ServiceRegistiration
    {
        


        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        { 

            services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<EcommerceAPIDbContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(12);
            });

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
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository,BasketItemWriteRepository>();


            services.AddScoped<IUserService, UserService >();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuth, AuthService>();
            services.AddScoped<IInternalAuth, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();

        }
    }
}
