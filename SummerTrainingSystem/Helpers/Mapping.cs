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
            CreateMap<HrCompany, CompanyVM>().ReverseMap();
            CreateMap<CompanySize, CompanySizeVM>().ReverseMap();
            CreateMap<TrainingType, TrainingTypeVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();
            CreateMap<Supervisor, SupervisorVM>().ReverseMap();
            CreateMap<IdentityRole, CreateRoleVM>().ReverseMap();
            CreateMap<Student, SaveStudentAccountVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<HrCompany, SaveCompanyAccountVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<HrCompany, EditHrCompanyVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Supervisor, SaveSupervisorAccountVM>().ForMember(m => m.Email, s => s.MapFrom(su => su.UserName)).ReverseMap();
            CreateMap<Student, EditStudentProfileVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Supervisor, EditSupervisorProfileVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Department, CreateDepartmentVM>().ReverseMap();
            CreateMap<Comment, CommentVM>().ReverseMap();
        }
    }
}
