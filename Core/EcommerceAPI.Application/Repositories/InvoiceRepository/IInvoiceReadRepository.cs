﻿using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Repositories.InvoiceRepository
{
    public interface IInvoiceReadRepository : IReadRepository<InvoiceFile>
    {
    }
}
