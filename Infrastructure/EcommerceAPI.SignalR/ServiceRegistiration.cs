using EcommerceAPI.Application.Abstraction.Hubs.OrderHub;
using EcommerceAPI.Application.Abstraction.Hubs.ProductHub;
using EcommerceAPI.SignalR.HubServices.OrderHubService;
using EcommerceAPI.SignalR.HubServices.ProductHubService;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.SignalR
{
    public static class ServiceRegistiration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddSignalR();
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IOrderHubService, OrderHubService>();
        }
    }
}
