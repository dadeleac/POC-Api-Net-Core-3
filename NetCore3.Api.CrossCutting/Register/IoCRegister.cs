using Microsoft.Extensions.DependencyInjection;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Application.Contracts.Helpers;
using NetCore3.Api.Application.Services;
using NetCore3.Api.Application.Services.Helpers;
using NetCore3.Api.DataAccess.Contracts;
using NetCore3.Api.DataAccess.Repositories;


namespace NetCore3.Api.CrossCutting.Register
{
    public static class IoCRegister
    {
        public static void AddRegisterIoC(this IServiceCollection services)
        {
            AddServices(services);
            AddHelperServices(services); 
            AddRepositories(services);
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentService, StudentService>();
        }

        private static void AddHelperServices(this IServiceCollection services)
        {
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
