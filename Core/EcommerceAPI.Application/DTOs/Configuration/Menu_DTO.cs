using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Configuration
{
    public class Menu_DTO
    {
        public string MenuName { get; set; }
        public List<Action_DTO> Actions { get; set; } = new();
    }
}
