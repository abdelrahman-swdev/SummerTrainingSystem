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
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
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

                if (!context.Trainnings.Any())
                {
                    IEnumerable<Trainning> trainnings = new List<Trainning>()
                    {
                        new Trainning{
                            Title ="Junior.Net Web Developer",
                            Description = @"Work with team in building web services and web-based applications using .NET technologies.
                                    Define, design, and implement multi-tiered object-oriented distributed software applications.
                                    Produce clean, efficient code based on client specifications.
                                    Integrate software components and third-party programs to meet specifications.",
                            DepartmentId = 1,
                            CreatedAt = DateTime.UtcNow,
                            StartAt = DateTime.UtcNow,
                            EndAt = DateTime.UtcNow.AddDays(90),
                            CompanyId="26fd4d66-3953-46c6-8eef-f139c6a3d4fe",
                            TrainingTypeId=1,
                        },

                        new Trainning{
                            Title ="System Analyst",
                            Description = @"Provide documentation of all processes and training as needed.
                                Liaising with users to track additional requirements and features.
                                Examining and evaluating current systems.",
                            DepartmentId = 2,
                            CreatedAt = DateTime.UtcNow,
                            StartAt = DateTime.UtcNow,
                            EndAt = DateTime.UtcNow.AddDays(60),
                            CompanyId="26fd4d66-3953-46c6-8eef-f139c6a3d4fe",
                            TrainingTypeId=2,
                        },

                        new Trainning{
                            Title ="IT Cyber Security Engineer",
                            Description = @"Optimization of cybersecurity solutions.
                                Troubleshooting security and network/systems problems.
                                Responding to all system and/or network/System security breaches.
                                Participating in the change management process.",
                            DepartmentId = 3,
                            CreatedAt = DateTime.UtcNow,
                            StartAt = DateTime.UtcNow,
                            EndAt = DateTime.UtcNow.AddDays(45),
                            CompanyId="26fd4d66-3953-46c6-8eef-f139c6a3d4fe",
                            TrainingTypeId=1,
                        }
                    };

                    await context.Trainnings.AddRangeAsync(trainnings);
                    await context.SaveChangesAsync();
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
