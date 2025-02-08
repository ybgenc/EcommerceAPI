using MediatR;
using Microsoft.AspNetCore.Http;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductİmage
{
    public class UploadProductImageRequest : IRequest<UploadProductImageResponse>
    {
        public string Id { get; set; }

        public IFormFileCollection? Files { get; set; }
    }
}
