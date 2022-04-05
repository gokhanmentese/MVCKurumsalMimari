
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class EGYSContext : DbContext
    {
        //public EGYSContext(DbContextOptions<EGYSContext> dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var config = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json")
            //   .Build();

            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            optionsBuilder
                //.UseLazyLoadingProxies()
                // .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                .UseSqlServer(@"Server=srv;Database=EGYS; User ID=egysadmin;Password=asd123!;");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<AssignTask> AssignTasks { get; set; }

        public DbSet<TaskFile> TaskFiles { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Directorship> Directorships { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<Email> Email { get; set; }

        public DbSet<EmailServerInfo> EmailServerInfo { get; set; }

     
    }
}
