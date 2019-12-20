using Microsoft.EntityFrameworkCore;
using NetCore3.Api.DataAccess.Contracts;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MoocDbContext _context;

        public CourseRepository(MoocDbContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context)); 
        }

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> DeleteAsync(Course course)
        {
            if(course == null)
            {
                throw new ArgumentNullException(nameof(course)); 
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> ExistAsync(Guid courseId)
        {
            if(courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId)); 
            }

            return await _context.Courses.AnyAsync(x => x.Id == courseId);
        }

        public async Task<Course> GetAsync(Guid courseId)
        {
            if(courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId)); 
            }

            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId); 
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync(); 
        }

        public async Task<Course> GetCourseForAuthorAsync(Guid authorId, Guid courseId)
        {
            if(authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId)); 
            }

            if(courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId)); 
            }

            return await _context.Courses
                .Where(x => x.AuthorId == authorId &&
                            x.Id == courseId)
                .FirstOrDefaultAsync(); 

        }

        public Task<Course> UpdateAsync(Guid courseId, Course course)
        {
            throw new NotImplementedException();
        }

        public async Task AddCourseForAuthor(Guid authorId, Course course)
        {
            if(authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if(course == null)
            {
                throw new ArgumentNullException(nameof(course)); 
            }

            course.AuthorId = authorId;
            
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course); 
            await _context.SaveChangesAsync();
        }
    }
}
