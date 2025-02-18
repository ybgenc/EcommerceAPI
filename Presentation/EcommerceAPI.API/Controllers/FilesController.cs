using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("getBaseUrl")]
        public IActionResult getBaseUrl()
        {
            var url = _configuration["BaseStorageUrl"];

            if (string.IsNullOrEmpty(url))
                return NotFound("BaseStorageUrl is not found.");

            return Ok(new { url });
        }

    }
}
