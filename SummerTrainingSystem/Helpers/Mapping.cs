using AutoMapper;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystem.Models;

namespace SummerTrainingSystem.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trainning, SaveTrainingVM>().ReverseMap();
            CreateMap<Student, SaveStudentAccountVM>().ForMember(m => m.Email, s => s.MapFrom(st => st.UserName)).ReverseMap();
            CreateMap<Supervisor, SaveSupervisorAccountVM>().ForMember(m => m.Email, s => s.MapFrom(su => su.UserName)).ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();

        }
    }
}
