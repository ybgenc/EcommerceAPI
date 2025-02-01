using EcommerceAPI.Application.Abstraction.Storage;
using EcommerceAPI.Application.Abstraction.Token;
using EcommerceAPI.Infrastructure.Enums;
using EcommerceAPI.Infrastructure.Services;
using EcommerceAPI.Infrastructure.Services.Storage;
using EcommerceAPI.Infrastructure.Services.Storage.Azure;
using EcommerceAPI.Infrastructure.Services.Storage.Local;
using EcommerceAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure :
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS :
                   // serviceCollection.AddScoped<IStorage, AWS>();
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }

    }
}
