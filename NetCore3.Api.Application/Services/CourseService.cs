using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.DataAccess.Contracts;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, 
            IAuthorService authorService, 
            IMapper mapper)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));
            _authorService = authorService ??
                throw new ArgumentNullException(nameof(authorService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper)); 
        }

        public async Task<IEnumerable<CourseModel>> GetCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseModel>>(courses);
        }


        public async Task<CourseModel> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!await _authorService.AuthorExist(authorId))
            {
                return null;
            }

            var course = await _courseRepository.GetCourseForAuthorAsync(authorId, courseId);
            return _mapper.Map<CourseModel>(course);
        }

        public async Task<CourseModel> CreateCourseForAuthor(Guid authorId, CourseForCreationModel course)
        {
            if (!await _authorService.AuthorExist(authorId))
            {
                return null;
            }

            var courseEntity = _mapper.Map<Course>(course);
            await _courseRepository.AddCourseForAuthor(authorId, courseEntity);
            return _mapper.Map<CourseModel>(courseEntity);
        }

        public async Task<CourseModel> UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateModel course)
        {
            if (!await _authorService.AuthorExist(authorId))
            {
                return null;
            }

            var courseFromRepo = await _courseRepository.GetCourseForAuthorAsync(authorId, courseId);

            if(courseFromRepo == null)
            {
                var courseToAdd = _mapper.Map<Course>(course);
                courseToAdd.Id = courseId;

                await _courseRepository.AddCourseForAuthor(authorId, courseToAdd); 

            }
            _mapper.Map(course, courseFromRepo);
            await _courseRepository.UpdateCourse(courseFromRepo);

            return _mapper.Map<CourseModel>(courseFromRepo);

        }

        public async Task<CourseModel> PartialUptadeCourseForAuthor(Guid authorId, Guid courseId, JsonPatchDocument<CourseForUpdateModel> patchDocument)
        {
            if (!await _authorService.AuthorExist(authorId))
            {
                return null;
            }

            var courseFromRepo = await _courseRepository.GetCourseForAuthorAsync(authorId, courseId);

            if (courseFromRepo == null)
            {
                return null; 
            }

            var courseToPatch = _mapper.Map<CourseForUpdateModel>(courseFromRepo);
            patchDocument.ApplyTo(courseToPatch);

            _mapper.Map(courseToPatch, courseFromRepo);
            await _courseRepository.UpdateCourse(courseFromRepo);

            return _mapper.Map<CourseModel>(courseFromRepo);
        }

        public async Task<bool> DeleteCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!await _authorService.AuthorExist(authorId))
            {
                return false;
            }

            var courseFromRepo = await _courseRepository.GetCourseForAuthorAsync(authorId, courseId);
            if (courseFromRepo == null)
            {
                return false;
            }

            return await _courseRepository.DeleteAsync(courseFromRepo); 
        }
    }
}
