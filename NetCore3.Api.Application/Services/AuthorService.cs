using AutoMapper;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Application.Helpers;
using NetCore3.Api.Application.QueryParameters;
using NetCore3.Api.DataAccess.Contracts;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public AuthorService(IAuthorRepository authorRepository, 
            IMapper mapper, 
            IPropertyMappingService propertyMappingService)
        {
           _authorRepository = authorRepository ??
                throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ??
                 throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthors()
        {
            var authorList = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorModel>>(authorList); 
        }

        public async Task<AuthorModel> GetAuthor(Guid authorId)
        {
            var author = await _authorRepository.GetAsync(authorId); 
            return _mapper.Map<AuthorModel>(author); 
        }

        public async Task<PagedList<AuthorModel>> GetAuthors(AuthorQueryParameters queryParameters)
        {
            if(queryParameters == null)
            {
                throw new ArgumentNullException(nameof(queryParameters)); 
            }

            var authorsCollection = await _authorRepository.GetAllAsync(); 
            
            if (!string.IsNullOrWhiteSpace(queryParameters.Job))
            {
                var job = queryParameters.Job.Trim();
                authorsCollection = authorsCollection
                    .Where(x => x.Job == job); 
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
            {
                var searchQuery = queryParameters.SearchQuery.Trim();
                authorsCollection = authorsCollection
                    .Where(x => x.Job.Contains(searchQuery) ||
                    x.Name.Contains(searchQuery) ||
                    x.Surname.Contains(searchQuery)); 
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
            {
                if(queryParameters.OrderBy.ToLowerInvariant() == "name")
                {
                    authorsCollection = authorsCollection
                        .OrderBy(x => x.Name)
                        .ThenBy(x => x.Surname); 
                }


            }

            var collectionToReturn = _mapper.Map<IEnumerable<AuthorModel>>(authorsCollection);

            return PagedList<AuthorModel>.Create(collectionToReturn,
                queryParameters.PageNumber,
                queryParameters.PageSize); 
        }

        public async Task<AuthorModel> AddAuthor(AuthorForCreationModel authorModel)
        {
            if(authorModel == null)
            {
                throw new ArgumentNullException(nameof(authorModel)); 
            }

            var authorEntity = _mapper.Map<Author>(authorModel);

            await _authorRepository.AddAsync(authorEntity);

            return _mapper.Map<AuthorModel>(authorEntity);     
        }

        public async Task<bool> AuthorExist(Guid authorId)
        {
            return await _authorRepository.ExistAsync(authorId); 
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthors(IEnumerable<Guid> authorIds)
        {
            var authors = await _authorRepository.GetAuthorsByIds(authorIds);
            return _mapper.Map<IEnumerable<AuthorModel>>(authors); 
        }

        public async Task<IEnumerable<AuthorModel>> AddAuthors(IEnumerable<AuthorForCreationModel> authors)
        {
            if(authors == null)
            {
                throw new ArgumentNullException(nameof(authors)); 
            }

            var authorsEntities = _mapper.Map<IEnumerable<Author>>(authors); 

            foreach(var authorEntity in authorsEntities)
            {
                await _authorRepository.AddAsync(authorEntity); 
            }

            return _mapper.Map<IEnumerable<AuthorModel>>(authorsEntities); 
        }

        public async Task<bool> DeleteAuthor(Guid authorId)
        {
            var author = await _authorRepository.GetAsync(authorId);

            if (author == null)
            {
                return false;
            }

            return await _authorRepository.DeleteAsync(author); 
        }
    }
}
