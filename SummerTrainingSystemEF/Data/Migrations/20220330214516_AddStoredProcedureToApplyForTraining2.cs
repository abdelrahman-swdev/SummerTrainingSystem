using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class AddStoredProcedureToApplyForTraining2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE proc [dbo].[spApplyForTraining] @studentId nvarchar(450), @trId int
                                    as
                                    begin
	                                    insert into [dbo].[StudentTrainning] values(@studentId, @trId);
	                                    update [dbo].[Trainnings] 
	                                    set [dbo].[Trainnings].ApplicantsCount = [dbo].[Trainnings].ApplicantsCount + 1
	                                    where [dbo].[Trainnings].Id = @trId
                                    end"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC [dbo].[spApplyForTraining]");
        }
    }
}
