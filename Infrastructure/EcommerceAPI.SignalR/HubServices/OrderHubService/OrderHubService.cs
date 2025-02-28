﻿using EcommerceAPI.Application.Abstraction.Hubs.OrderHub;
using EcommerceAPI.SignalR.Hubs.OrderHub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.SignalR.HubServices.OrderHubService
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionsNames.OrderAddedMessage,message);

        }
    }
}
