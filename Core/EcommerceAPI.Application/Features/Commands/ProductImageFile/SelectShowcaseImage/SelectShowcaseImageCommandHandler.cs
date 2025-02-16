using EcommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.SelectShowcaseImage
{
    public class SelectShowcaseImageCommandHandler : IRequestHandler<SelectShowcaseImageCommandRequest, SelectShowcaseImageCommandResponse>
    {
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public SelectShowcaseImageCommandHandler(IProductImageWriteRepository productImageWriteRepository)
        {
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<SelectShowcaseImageCommandResponse> Handle(SelectShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.imageId, request.productId);

            var query = _productImageWriteRepository.Table
                                 .Include(p => p.Product)
                                 .SelectMany(p => p.Product, (pif, p) => new
                                 {
                                     pif,
                                     p
                                 });
            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.productId) && p.pif.ShowCase);
            if (data != null)
                data.pif.ShowCase = false;
            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.imageId));
            if (image != null)
                image.pif.ShowCase = true;
            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
