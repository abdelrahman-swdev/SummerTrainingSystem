using SummerTrainingSystemCore.Entities;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface ITrainingRepository : IGenericRepository<Trainning>
    {
        Task<int> ApplyForTraining(Student student, Trainning training);
        Task<bool> CheckIfStudentApplied(string studentUserName, int trainingId);
    }
}
