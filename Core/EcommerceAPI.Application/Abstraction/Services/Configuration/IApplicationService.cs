using EcommerceAPI.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Services.Configuration
{
    public interface IApplicationService
    {
        List<Menu_DTO> GetAuthorizeDefinitonEndpoints(Type type);
    }
}
