using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Persistence.Contexts;
using File = EcommerceAPI.Domain.Entities.File;

namespace EcommerceAPI.Persistence.Repositories.FileRepository
{
    public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
