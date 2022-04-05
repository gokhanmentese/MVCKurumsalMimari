using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Interfaces;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<EfAuthenticationDal>().As<IAuthenticationDal>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserProfileManager>().As<IUserProfileService>();
            builder.RegisterType<EfUserProfileDal>().As<IUserProfileDal>();

            builder.RegisterType<UserRoleManager>().As<IUserRoleService>();
            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();

            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<TaskManager>().As<ITaskService>();
            builder.RegisterType<EfTaskDal>().As<ITaskDal>();

            builder.RegisterType<DirectorshipManager>().As<IDirectorshipService>();
            builder.RegisterType<EfDirectorshipDal>().As<IDirectorshipDal>();

            builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
            builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>();

            builder.RegisterType<UnitManager>().As<IUnitService>();
            builder.RegisterType<EfUnitDal>().As<IUnitDal>();

            builder.RegisterType<EmailTemplateManager>().As<IEmailTemplateService>();
            builder.RegisterType<EfEmailTemplateDal>().As<IEmailTemplateDal>();

            builder.RegisterType<AssignTaskManager>().As<IAssignTaskService>();
            builder.RegisterType<EfAssignTaskDal>().As<IAssignTaskDal>();

            builder.RegisterType<EmailServerManager>().As<IEMaiIServerService>();
            builder.RegisterType<EfEmailServerDal>().As<IEmailServerDal>();

            builder.RegisterType<EmailManager>().As<IEmailService>();
            builder.RegisterType<EfEmailDal>().As<IEmailDal>();

            builder.RegisterType<TaskFileManager>().As<ITaskFileService>();
            builder.RegisterType<EfTaskFileDal>().As<ITaskFileDal>();

            builder.RegisterType<FileManager>().As<IFileService>();
            builder.RegisterType<EfFileDal>().As<IFileDal>();

            builder.RegisterType<DownloadManager>().As<IDownloadService>();

            builder.RegisterType<DropdownManager>().As<IDropdownService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }

    }
}
