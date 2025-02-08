
using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.InvoiceRepository
{
    public class InvoiceWriteRepository : WriteRepository<InvoiceFile>, IInvoiceWriteRepository
    {
        public InvoiceWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
