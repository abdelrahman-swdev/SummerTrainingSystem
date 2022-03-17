using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerTrainingSystemCore.Entities;

namespace SummerTrainingSystemEF.Data.EntitiesConfig
{
    public class HrCompanyConfig : IEntityTypeConfiguration<HrCompany>
    {
        public void Configure(EntityTypeBuilder<HrCompany> builder)
        {
            builder.ToTable("HrCompanies");
        }
    }
}
