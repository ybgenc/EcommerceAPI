using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Application
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistiration));
            collection.AddHttpClient();
        }
    }
}
