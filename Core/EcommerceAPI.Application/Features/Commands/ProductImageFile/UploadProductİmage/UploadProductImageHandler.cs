using EcommerceAPI.Application.Abstraction.Storage;
using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Application.Repositories.ProductRepository;
using MediatR;
using ProdcutImage = EcommerceAPI.Domain.Entities;
using Products = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductİmage
{
    public class UploadProductImageHandler : IRequestHandler<UploadProductImageRequest, UploadProductImageResponse>
    {
        readonly IProductImageWriteRepository _productImageWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IStorageService _storageService;

        public UploadProductImageHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageWriteRepository productImageWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<UploadProductImageResponse> Handle(UploadProductImageRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", request.Files);

            Products.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageWriteRepository.AddRangeAsync(result.Select(p => new ProdcutImage.ProductImageFile
            {
                FileName = p.fileName,
                Path = p.pathOrContainerName,
                Storage = _storageService.StorageName,
                Product = new List<Products.Product>() { product }
            }).ToList());
            await _productImageWriteRepository.SaveAsync();

            return new();
        }
    }
}
