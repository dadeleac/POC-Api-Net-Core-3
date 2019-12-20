using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore3.Api.DataAccess.Contracts
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseForAuthorAsync(Guid authorId, Guid courseId);
        Task AddCourseForAuthor(Guid authorId, Course course);
        Task UpdateCourse(Course course); 

    }
}
