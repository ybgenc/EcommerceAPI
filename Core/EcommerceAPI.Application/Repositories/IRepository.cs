using EcommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntitiy
    {
         DbSet<T> Table { get; } 
    }
}
