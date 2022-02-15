using AutoMapper;
using SummerTrainingSystem.Data.Entities;
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
