using Microsoft.AspNetCore.JsonPatch;
using NetCore3.Api.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetCourses();
        Task<CourseModel> GetCourseForAuthor(Guid authorId, Guid courseId);
        Task<CourseModel> CreateCourseForAuthor(Guid authorId, CourseForCreationModel course);
        Task<CourseModel> UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateModel course);
        Task<CourseModel> PartialUptadeCourseForAuthor(Guid authorId, Guid courseId, JsonPatchDocument<CourseForUpdateModel> patchDocument);
        Task<bool> DeleteCourseForAuthor(Guid authorId, Guid courseId); 


    }
}
