using MediatR;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
    {
        public string id { get; set; }
        public string imageId { get; set; }
    }
}
