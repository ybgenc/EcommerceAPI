using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = EcommerceAPI.Domain.Entities.File;

namespace EcommerceAPI.Application.Repositories.FileRepository
{
    public interface IFileReadRepository : IReadRepository<File>
    {
    }
}
