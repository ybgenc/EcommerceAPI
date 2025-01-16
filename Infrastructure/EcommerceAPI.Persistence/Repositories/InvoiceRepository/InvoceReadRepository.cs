using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.InvoiceRepository
{
    public class InvoceReadRepository : ReadRepository<InvoiceFile>, IInvoiceReadRepository
    {
        public InvoceReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
