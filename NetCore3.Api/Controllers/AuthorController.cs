using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Application.QueryParameters;
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

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService ??
                throw new ArgumentNullException(nameof(authorService)); 
        }

        [HttpGet(Name = "GetAuthors")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthors(
            [FromQuery] AuthorQueryParameters queryParameters)
        {
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

            return Ok(authors); 
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<AuthorModel>> GetAuthor(Guid authorId)
        {
            var author = await _authorService.GetAuthor(authorId)
                .ConfigureAwait(false); 

            if(author == null)
            {
                return NotFound();
            }

            return Ok(author); 
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
                            pageNumber = queryParameters.PageNumber - 1,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                        new
                        {
                            pageNumber = queryParameters.PageNumber + 1,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
                default:
                    return Url.Link("GetAuthors",
                        new
                        {
                            pageNumber = queryParameters.PageNumber,
                            pageSize = queryParameters.PageSize,
                            job = queryParameters.Job,
                            searchQuery = queryParameters.SearchQuery
                        });
            }
        }
    }
}