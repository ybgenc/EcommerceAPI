using EcommerceAPI.Application.Abstraction.Services.Configuration;
using EcommerceAPI.Application.Attributes.Custom;
using EcommerceAPI.Application.DTOs.Configuration;
using EcommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Services.Configuration
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu_DTO> GetAuthorizeDefinitonEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type); // Current assembly
            var controllers = assembly.GetTypes().Where(c => c.IsAssignableTo(typeof(ControllerBase))); //Select controllers from all currently running types
            List<Menu_DTO> menus = new();
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var AuthorizedActions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute))); // Select controllers that have methods marked with the AuthorizeDefinitionAttribute
                    if (AuthorizedActions != null)
                    {
                        foreach (var actions in AuthorizedActions)
                        {
                            var attributes = actions.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu_DTO menu = null;

                                var authorizeDefinitionAttriute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.MenuName == authorizeDefinitionAttriute.Menu))
                                {
                                    menu = new() { MenuName = authorizeDefinitionAttriute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.MenuName == authorizeDefinitionAttriute.Menu);

                                Action_DTO action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttriute.ActionType),
                                    Definition = authorizeDefinitionAttriute.Definition
                                };


                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    action.HttpType = HttpMethods.Get;

                                action.ActionCode = $"{action.HttpType}-{action.ActionType}:{action.Definition.Replace(" ", "_")}";

                                menu.Actions.Add(action);

                            }
                        }
                    }
                }

            }

            return menus;
        }
    }
}
