using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerTrainingSystemCore.Entities;

namespace SummerTrainingSystemEF.Data.EntitiesConfig
{
    public class TrainingConfig : IEntityTypeConfiguration<Trainning>
    {
        public void Configure(EntityTypeBuilder<Trainning> builder)
        {
            // when company is deleted => delete its trainings also
            builder.HasOne(t => t.Company)
                .WithMany()
                .HasForeignKey(t => t.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
