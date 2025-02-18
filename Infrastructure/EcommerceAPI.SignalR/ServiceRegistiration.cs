using EcommerceAPI.Application.Abstraction.Hubs;
using EcommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.SignalR
{
    public static class ServiceRegistiration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddSignalR();
            collection.AddTransient<IProductHubService, ProductHubService>();
        }
    }
}
