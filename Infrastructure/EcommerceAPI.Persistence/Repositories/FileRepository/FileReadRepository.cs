using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Persistence.Contexts;
using File = EcommerceAPI.Domain.Entities.File;

namespace EcommerceAPI.Persistence.Repositories.FileRepository
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
