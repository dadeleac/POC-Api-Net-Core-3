using NetCore3.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.Contracts.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorModel>> GetAuthors();
        Task<AuthorModel> GetAuthor(Guid authorId); 
    }
}
