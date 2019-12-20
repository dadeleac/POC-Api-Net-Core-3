using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<bool> ExistAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task AddAsync(T element);
        Task<T> UpdateAsync(Guid id, T element);
        Task<bool> DeleteAsync(T element);
    }
}
