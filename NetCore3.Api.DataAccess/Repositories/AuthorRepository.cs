using Microsoft.EntityFrameworkCore;
using NetCore3.Api.DataAccess.Contracts;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MoocDbContext _context;

        public AuthorRepository(MoocDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException(nameof(author)); 
            }

            author.Id = Guid.NewGuid(); 

            foreach(var course in author.Courses)
            {
                course.Id = Guid.NewGuid(); 
            }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> DeleteAsync(Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> ExistAsync(Guid authorId)
        {
            if(authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors.AnyAsync(x => x.Id == authorId); 
        }

        public async Task<Author> GetAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId); 
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync(); 
        }

        public Task<Author> UpdateAsync(Guid id, Author element)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByIds(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return await _context.Authors
                .Where(x => authorIds.Contains(x.Id))
                .ToListAsync();
        }


    }
}
