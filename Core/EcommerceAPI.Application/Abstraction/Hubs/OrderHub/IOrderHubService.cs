using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Hubs.OrderHub
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);

    }
}
