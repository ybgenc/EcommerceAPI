using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Hubs.ProductHub
{
    public interface IProductHubService
    {
        Task ProductAddedMessageAsync(string message);
    }
}
