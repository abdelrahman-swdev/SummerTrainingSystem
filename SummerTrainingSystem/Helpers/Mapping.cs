using AutoMapper;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystem.Models;

namespace SummerTrainingSystem.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trainning, TrainingVM>().ReverseMap();
        }
    }
}
