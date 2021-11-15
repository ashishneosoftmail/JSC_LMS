using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Infrastructure.EncryptDecrypt;
using JSC_LMS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JSC_LMS.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IPrincipalRepository, PrincipalRepository>();

            services.AddScoped<IZipRepository, ZipRepository>();

            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISuperadminRepository, SuperadminRepository>();
            services.AddScoped<IKnowledgeBaseRepository, KnowledgeBaseRepository>();



            return services;
        }
    }
}
