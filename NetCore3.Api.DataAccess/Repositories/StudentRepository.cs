using Microsoft.EntityFrameworkCore;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MoocDbContext _context;

        public StudentRepository(MoocDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            return await _context.Students.AnyAsync(x => x.Id == studentId); 
        }

        public async Task<Student> GetAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            return await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId); 
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync(); 
        }

        public Task<Student> UpdateAsync(Guid id, Student element)
        {
            throw new NotImplementedException();
        }
    }
}
