using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Domain.Models.Author;
using NetCore3.Api.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorCollectionController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorCollectionController(IAuthorService authorService)
        {
            _authorService = authorService ??
                throw new ArgumentNullException(nameof(authorService));
        }

        [HttpGet("({ids})", Name = "GetAuthorCollections")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthorCollections(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                return BadRequest(); 
            }

            var authors = await _authorService.GetAuthors(ids)
                .ConfigureAwait(false); 

            if(ids.Count() != authors.Count())
            {
                return NotFound(); 
            }   
            
            return Ok(authors); 
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> CreateAuthorCollection(IEnumerable<AuthorForCreationModel> authorCollection)
        {
            var authors = await _authorService.AddAuthors(authorCollection)
                .ConfigureAwait(false);

            if(authors == null)
            {
                return NotFound();
            }

            var idsAsString = string.Join(",", authors.Select(x => x.Id));

            return Ok(authors); 
        }
    }
}
