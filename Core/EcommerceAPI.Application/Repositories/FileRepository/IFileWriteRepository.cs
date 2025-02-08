using File = EcommerceAPI.Domain.Entities.File;

namespace EcommerceAPI.Application.Repositories.FileRepository
{
    public interface IFileWriteRepository : IWriteRepository<File>
    {
    }
}
