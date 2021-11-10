using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Services.IRepositories;
using JSC_LSM.UI.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JSC_LSM.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //_aPIRepository = aPIRepository;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Refresh View without restarting application
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            // ApiBaseUrl Keys
            services.Configure<ApiBaseUrl>(Configuration.GetSection("ApiBaseUrl"));


            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>(s => new UserRepository());
            services.AddScoped<IRoleRepository, RolesRepository>(s => new RolesRepository());
            services.AddScoped<IStateRepository, StatesRepository>(s => new StatesRepository());
            services.AddScoped<Common.Common>();
            services.AddScoped<ICityRepository, CitiesRepository>(s => new CitiesRepository());
            services.AddScoped<IZipRepository, ZipRepository>(s => new ZipRepository());
            services.AddScoped<IInstituteRepository, InstituteRepository>(s => new InstituteRepository());
            services.AddScoped<ISchoolRepository, SchoolRepository>(s => new SchoolRepository());
            services.AddScoped<IPrincipalRepository, PrincipalRepository>(s => new PrincipalRepository());
            services.AddScoped<IClassRepository, ClassRepository>(s => new ClassRepository());
            services.AddScoped<ISectionRepository, SectionRepository>(s => new SectionRepository());
            services.AddScoped<ITeacherRepository, TeacherRepository>(s => new TeacherRepository());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
