using AutoMapper;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Course;

namespace NetCore3.Api.Application.Mappers
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseModel>();
            CreateMap<CourseModel, Course>();
            CreateMap<CourseForCreationModel, Course>();
            CreateMap<CourseForUpdateModel, Course>();
            CreateMap<Course, CourseForUpdateModel>(); 
        }
    }
}
