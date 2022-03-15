using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystemCore.Entities;

namespace SummerTrainingSystemEF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Trainning> Trainnings { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Supervisor> Supervisors { get; set; }
        public virtual DbSet<HrCompany> HrCompanies { get; set; }
        public virtual DbSet<CompanySize> CompanySizes { get; set; }
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<Student>()
                .ToTable("Students")
                .HasIndex(s => s.UniversityID)
                .IsUnique();

            builder.Entity<Supervisor>()
                .ToTable("Supervisors")
                .HasIndex(s => s.UniversityID)
                .IsUnique();

            builder.Entity<HrCompany>()
                .ToTable("HrCompanies");


        }
    }
}
