using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Data.SeedData
{
    public static class Seeding
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Departments.Any())
                {
                    IEnumerable<Department> deps = new List<Department>()
                    {
                        new Department{Name = "Information Systems", Abbreviation = "IS"},
                        new Department{Name = "Computer Science", Abbreviation = "CS"},
                        new Department{Name = "Information Technology", Abbreviation = "IT"}
                    };
                    await context.Departments.AddRangeAsync(deps);
                    await context.SaveChangesAsync();
                }

                if (!context.TrainingTypes.Any())
                {
                    IEnumerable<TrainingType> types = new List<TrainingType>()
                    {
                        new TrainingType{TypeName="Full Time"},
                        new TrainingType{TypeName="Remote"},
                    };
                    await context.TrainingTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }

                if (!context.Roles.Any())
                {
                    IEnumerable<IdentityRole> roles = new List<IdentityRole>()
                    {
                        new IdentityRole{Name = Roles.Admin.ToString(), NormalizedName = Roles.Admin.ToString().ToUpper()},
                        new IdentityRole{Name = Roles.Student.ToString(), NormalizedName = Roles.Student.ToString().ToUpper()},
                        new IdentityRole{Name = Roles.Supervisor.ToString(), NormalizedName = Roles.Supervisor.ToString().ToUpper()},
                        new IdentityRole{Name = Roles.Company.ToString(), NormalizedName = Roles.Company.ToString().ToUpper()},
                    };
                    await context.Roles.AddRangeAsync(roles);
                    await context.SaveChangesAsync();
                }

                if (!context.CompanySizes.Any())
                {
                    IEnumerable<CompanySize> sizes = new List<CompanySize>()
                    {
                        new CompanySize{SizeName="Small", SizeRange="10 to 49 employees"},
                        new CompanySize{SizeName="Medium", SizeRange="50 to 249 employees"},
                        new CompanySize{SizeName="Large", SizeRange="250 employees or more"}
                    };
                    await context.CompanySizes.AddRangeAsync(sizes);
                    await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {
                    var student = new Student()
                    {
                        FirstName = "student",
                        LastName = "student",
                        Email = "student@stu.com",
                        UserName = "student@stu.com",
                        UniversityID = 10,
                        Gpa = 3,
                        Level = 1,
                        PhoneNumber = "0123",
                        DepartmentId = 1
                    };
                    var supervisor = new Supervisor()
                    {
                        FirstName = "super",
                        LastName = "visor",
                        Email = "visor@sup.com",
                        UserName = "visor@sup.com",
                        UniversityID = 20,
                        PhoneNumber = "0123",
                        DepartmentId = 2
                    };
                    var admin = new IdentityUser()
                    {
                        Email = "admin@admin.com",
                        UserName = "admin@admin.com",
                        PhoneNumber = "0123"
                    };

                    var password = "Pa$$w0rd";

                    var stResult = await userManager.CreateAsync(student, password);
                    if(stResult.Succeeded) await userManager.AddToRoleAsync(student, Roles.Student.ToString());

                    var supResult = await userManager.CreateAsync(supervisor, password);
                    if (supResult.Succeeded) await userManager.AddToRoleAsync(supervisor, Roles.Supervisor.ToString());

                    var adminResult = await userManager.CreateAsync(admin, password);
                    if (adminResult.Succeeded) await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}
