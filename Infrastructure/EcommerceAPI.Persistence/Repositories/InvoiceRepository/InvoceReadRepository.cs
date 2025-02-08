using EcommerceAPI.Application.Repositories.InvoiceRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.InvoiceRepository
{
    public class InvoceReadRepository : ReadRepository<InvoiceFile>, IInvoiceReadRepository
    {
        public InvoceReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
