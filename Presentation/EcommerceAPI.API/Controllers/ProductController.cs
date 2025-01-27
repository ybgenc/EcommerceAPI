using EcommerceAPI.Application.Abstraction.Storage;
using EcommerceAPI.Application.Features.Commands.Product.CreateProduct;
using EcommerceAPI.Application.Features.Querys.Product.GetAllProduct;
using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Application.Repositories.ProductRepository;
using EcommerceAPI.Application.ViewModels.Products;
using EcommerceAPI.Domain.Entities;
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
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;
        readonly IProductImageReadRepository _productImageReadRepository;
        readonly IInvoiceWriteRepository _invoiceWriteRepository;
        readonly IInvoiceReadRepository _invoiceReadRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration _configuration;

        readonly IMediator _mediator;



        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageWriteRepository productImageWriteRepository, IProductImageReadRepository productImageReadRepository, IInvoiceWriteRepository invoiceWriteRepository, IInvoiceReadRepository invoiceReadRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _invoiceReadRepository = invoiceReadRepository;
            _invoiceWriteRepository = invoiceWriteRepository;
            _productImageReadRepository = productImageReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
           GetAllProductQueryResponse response =  await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response =  await _mediator.Send(createProductCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(VMUpdateProduct model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.DeleteAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm] IFormFileCollection files, [FromForm] string id)
        {

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageWriteRepository.AddRangeAsync(result.Select(p => new ProductImageFile
            {
                FileName = p.fileName,
                Path = p.pathOrContainerName,
                Storage = _storageService.StorageName,
                Product = new List<Product>() { product }
            }).ToList());
            await _productImageWriteRepository.SaveAsync();

            await _productWriteRepository.SaveAsync();
            return Ok();


        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {

            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImageFiles.Remove(productImageFile);
            await _productImageWriteRepository.SaveAsync();
            return Ok();

        }
        
        





    }
}
