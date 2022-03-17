using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerTrainingSystemCore.Entities;

namespace SummerTrainingSystemEF.Data.EntitiesConfig
{
    public class SupervisorConfig : IEntityTypeConfiguration<Supervisor>
    {

        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {
           builder.ToTable("Supervisors")
                    .HasIndex(s => s.UniversityID)
                    .IsUnique();

            builder.HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
