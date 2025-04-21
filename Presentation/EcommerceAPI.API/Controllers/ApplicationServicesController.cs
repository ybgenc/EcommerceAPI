using EcommerceAPI.Application.Abstraction.Services.Configuration;
using EcommerceAPI.Application.Attributes.Custom;
using EcommerceAPI.Application.Consts;
using EcommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu =AuthorizeDefinitonConstants.ApplicationServices, ActionType =ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var endpoints = _applicationService.GetAuthorizeDefinitonEndpoints(typeof(Program));
            return Ok(endpoints);
        }
    }
}
