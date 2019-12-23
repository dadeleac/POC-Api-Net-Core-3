using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Application.Contracts.Helpers;
using NetCore3.Api.Application.Helpers;
using NetCore3.Api.Application.QueryParameters;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Author;
using NetCore3.Api.Helpers;

namespace NetCore3.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public AuthorController(IAuthorService authorService, 
            IPropertyMappingService propertyMappingService, 
            IPropertyCheckerService propertyCheckerService)
        {
            _authorService = authorService ??
                throw new ArgumentNullException(nameof(authorService));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService)); 

        }

        [HttpGet(Name = "GetAuthors")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetAuthors(
            [FromQuery] AuthorQueryParameters queryParameters)
        {

            if (!_propertyMappingService.ValidMappingExistFor<AuthorModel, Author>(queryParameters.OrderBy))
            {
                return BadRequest(); 
            }

            if (!_propertyCheckerService.TypeHasProperties<AuthorModel>(queryParameters.Fields))
            {
                return BadRequest(); 
            }

            var authors = await _authorService.GetAuthors(queryParameters)
                .ConfigureAwait(false);

            var previousPageLink = authors.HasPrevious ?
                CreateAuthorsResourceUri(queryParameters, ResourceUriType.PreviousPage) : null;

            var nextPageLink = authors.HasNext ?
                CreateAuthorsResourceUri(queryParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = authors.TotalCount,
                pageSize = authors.PageSize,
                currentPage = authors.CurrentPage,
                totalPages = authors.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata)); 

            return Ok(authors.ShapeEnumerableData(queryParameters.Fields)); 
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetAuthor(Guid authorId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<AuthorModel>(fields))
            {
                return BadRequest();
            }

            var author = await _authorService.GetAuthor(authorId)
                .ConfigureAwait(false); 

            if(author == null)
            {
                return NotFound();
            }

            return Ok(author.ShapeData(fields)); 
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AuthorModel>> CreateAuthor(AuthorForCreationModel author)
        {
            var authorCreation = await _authorService.AddAuthor(author)
                .ConfigureAwait(false);

            return CreatedAtRoute("GetAuthor",
                new { authorId = authorCreation.Id },
                authorCreation); 
        }
       
        [HttpDelete("{authorId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            if (!await _authorService.DeleteAuthor(authorId).ConfigureAwait(false))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        private string CreateAuthorsResourceUri(AuthorQueryParameters queryParameters, 
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                        new
                        {
                            fields = queryParameters.Fields, 
                            orderBy = queryParameters.OrderBy,
                            pageNumber = queryParameters.PageNumber - 1,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                        new
                        {
                            fields = queryParameters.Fields,
                            orderBy = queryParameters.OrderBy, 
                            pageNumber = queryParameters.PageNumber + 1,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
                default:
                    return Url.Link("GetAuthors",
                        new
                        {
                            fields = queryParameters.Fields,
                            orderBy = queryParameters.OrderBy,
                            pageNumber = queryParameters.PageNumber,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
            }
        }
    }
}