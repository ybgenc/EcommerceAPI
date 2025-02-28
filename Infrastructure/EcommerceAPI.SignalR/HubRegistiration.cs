using EcommerceAPI.SignalR.Hubs.OrderHub;
using EcommerceAPI.SignalR.Hubs.ProductHub;
using Microsoft.AspNetCore.Builder;

namespace EcommerceAPI.SignalR
{
    public static class HubRegistiration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
