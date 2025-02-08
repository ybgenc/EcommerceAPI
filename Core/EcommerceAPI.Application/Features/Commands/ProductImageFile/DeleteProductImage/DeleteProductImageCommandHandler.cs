using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using EcommerceAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductImage = EcommerceAPI.Domain.Entities;
using Products = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageWriteRepository productImageWriteRepository = null)
        {
            _productReadRepository = productReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {

            Products.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));

            ProductImage.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.imageId));

            if(productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);
            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
