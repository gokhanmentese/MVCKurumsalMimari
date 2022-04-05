using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins(""));
            //});

           // Injections(services);

            services.AddControllers();

            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule() 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(builder=>builder.WithOrigins("http://localhost:44362").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Web API");
                //});
            });
        }

        public void Injections(IServiceCollection services)
        {
            #region Services
            //services.AddSingleton<IAuthService>(provider =>
            //{
            //    var dataService = new EfAuthenticationDal();
            //    return new AuthManager(dataService);
            //});

            services.AddSingleton<IUserService>(provider =>
            {
                var dataService = new EfUserDal();
                return new UserManager(dataService);
            });

            services.AddSingleton<IUserProfileService>(provider =>
            {
                var dataService = new EfUserProfileDal();
                return new UserProfileManager(dataService);
            });

            services.AddSingleton<ICategoryService>(provider =>
            {
                var dataService = new EfCategoryDal();
                return new CategoryManager(dataService);
            });

            services.AddSingleton<ITaskService>(provider =>
            {
                var dataService = new EfTaskDal();
                return new TaskManager(dataService);
            });

            services.AddSingleton<IDirectorshipService>(provider =>
            {
                var dataService = new EfDirectorshipDal();
                return new DirectorshipManager(dataService);
            });

            services.AddSingleton<IDepartmentService>(provider =>
            {
                var dataService = new EfDepartmentDal();
                return new DepartmentManager(dataService);
            });

            services.AddSingleton<IUnitService>(provider =>
            {
                var dataService = new EfUnitDal();
                return new UnitManager(dataService);
            });

            services.AddSingleton<IEmailTemplateService>(provider =>
            {
                var dataService = new EfEmailTemplateDal();
                return new EmailTemplateManager(dataService);
            });
            #endregion
        }

    }
}
