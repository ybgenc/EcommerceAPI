using EcommerceAPI.Domain.Entities.Common;

namespace EcommerceAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntitiy
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> model);
        bool Delete(T model);
        bool DeleteRange(List<T> model);
        Task<bool> DeleteAsync(string Id);
        bool Update(T model);
        Task<int> SaveAsync();

         
    }
}
 