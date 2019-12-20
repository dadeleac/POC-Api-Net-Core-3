using AutoMapper;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Author;

namespace NetCore3.Api.Application.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));

            CreateMap<AuthorModel, Author>();

            CreateMap<AuthorForCreationModel, Author>(); 
        }
     
    }
}
