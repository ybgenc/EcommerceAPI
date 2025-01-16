using EcommerceAPI.Application.Repositories.FileRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
