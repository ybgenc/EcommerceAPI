using EcommerceAPI.Application.Features.Commands.Product.CreateProduct;
using EcommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using EcommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using EcommerceAPI.Application.Features.Commands.ProductImageFile.SelectShowcaseImage;
using EcommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductİmage;
using EcommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using EcommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using EcommerceAPI.Application.Features.Queries.Product.GetProductImage;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {

        readonly IMediator _mediator;
        readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {

            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Upload([FromForm] UploadProductImageRequest uploadProductImageRequest)
        {
            uploadProductImageRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImageRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetProductImage([FromRoute] GetProductImageQueryRequest getProductImageQueryRequest)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImageQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();

        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> selectShowcaseImage([FromQuery] SelectShowcaseImageCommandRequest selectShowcaseImageCommandRequest)

        {
            SelectShowcaseImageCommandResponse response = await _mediator.Send(selectShowcaseImageCommandRequest);
            return Ok(response);
        }
    }
}
