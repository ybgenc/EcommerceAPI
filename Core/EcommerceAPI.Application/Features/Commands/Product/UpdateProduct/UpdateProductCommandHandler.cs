using EcommerceAPI.Application.Repositories.ProductRepository;
using MediatR;
using Products = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {

            Products.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Description = request.Description;
            product.Title = request.Title;
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
