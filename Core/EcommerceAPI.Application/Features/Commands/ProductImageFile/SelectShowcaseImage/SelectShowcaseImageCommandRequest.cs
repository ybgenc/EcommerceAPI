﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.SelectShowcaseImage
{
    public class SelectShowcaseImageCommandRequest : IRequest<SelectShowcaseImageCommandResponse>
    {
        public string imageId { get; set; }
        public string productId { get; set; }
    }
}
