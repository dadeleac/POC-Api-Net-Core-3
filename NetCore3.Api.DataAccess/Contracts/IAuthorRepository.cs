using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsByIds(IEnumerable<Guid> authorIds);

    }
}
