using EcommerceAPI.Application.Abstraction.Storage;
using EcommerceAPI.Application.Features.Commands.Product.CreateProduct;
using EcommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using EcommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using EcommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductİmage;
using EcommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using EcommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using EcommerceAPI.Application.Features.Queries.Product.GetProductImage;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        readonly IMediator _mediator;



        public ProductController(IMediator mediator)
        {

            _mediator = mediator;
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
        public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm] UploadProductImageRequest uploadProductImageRequest)
        {
            uploadProductImageRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImageRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImage([FromRoute]GetProductImageQueryRequest getProductImageQueryRequest)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImageQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();

        }
        
        





    }
}
