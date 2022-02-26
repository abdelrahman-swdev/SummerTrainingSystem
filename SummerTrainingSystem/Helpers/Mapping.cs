using AutoMapper;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace SummerTrainingSystem.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trainning, SaveTrainingVM>().ReverseMap();
            CreateMap<Trainning, TrainingVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();
            CreateMap<IdentityRole, CreateRoleVM>().ReverseMap();
            CreateMap<Student, SaveStudentAccountVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Supervisor, SaveSupervisorAccountVM>().ForMember(m => m.Email, s => s.MapFrom(su => su.UserName)).ReverseMap();
            CreateMap<Student, EditStudentProfileVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Supervisor, EditSupervisorProfileVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
        }
    }
}
