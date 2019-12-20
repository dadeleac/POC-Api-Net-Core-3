using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Exist(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task Add(T element);
        Task<T> Update(Guid id, T element);
        Task DeleteAsync(T element);
    }
}
