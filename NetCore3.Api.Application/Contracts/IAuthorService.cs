using NetCore3.Api.Application.Helpers;
using NetCore3.Api.Application.QueryParameters;
using NetCore3.Api.Domain.Models.Author;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.Contracts
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorModel>> GetAuthors(AuthorQueryParameters queryParameters);
        Task<IEnumerable<AuthorModel>> GetAuthors(IEnumerable<Guid> ids); 
        Task<AuthorModel> GetAuthor(Guid authorId);
        Task<AuthorModel> AddAuthor(AuthorForCreationModel author);
        Task<bool> AuthorExist(Guid authorId);
        Task<IEnumerable<AuthorModel>> AddAuthors(IEnumerable<AuthorForCreationModel> authors);
        Task<bool> DeleteAuthor(Guid authorId); 

    }
}
