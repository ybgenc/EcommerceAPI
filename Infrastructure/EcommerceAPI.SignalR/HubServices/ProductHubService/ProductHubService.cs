using EcommerceAPI.Application.Abstraction.Hubs.ProductHub;
using EcommerceAPI.SignalR.Hubs.ProductHub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.SignalR.HubServices.ProductHubService
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubcontext;

        public ProductHubService(IHubContext<ProductHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubcontext.Clients.All.SendAsync(ReceiveFunctionsNames.ProductAddedMessage, message);
        }
    }
}
