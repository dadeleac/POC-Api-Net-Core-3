using AutoMapper;
using NetCore3.Api.Application.Helpers;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Student;

namespace NetCore3.Api.Application.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ForMember(
                    dest => dest.Age, 
                    opt => opt.MapFrom(src => src.Birthday.GetCurrentAge())); 
        }
    }
}
