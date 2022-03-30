using SummerTrainingSystemCore.Entities;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface ITrainingRepository : IGenericRepository<Trainning>
    {
        int ApplyForTraining(string studentId, int trainingId);
        bool CheckIfStudentApplied(string studentUserName, int trainingId);
    }
}
