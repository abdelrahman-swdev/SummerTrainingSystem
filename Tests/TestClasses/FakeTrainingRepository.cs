using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;

namespace Tests.TestClasses
{
    public class FakeTrainingRepository : FakeIGenericRepository<Trainning>, ITrainingRepository
    {
        private Trainning fakeInstance;
        private int applyForTraining;
        private bool checkIfStudentApplied;

        public FakeTrainingRepository(Trainning instance = null) : base(instance) { }
        public FakeTrainingRepository(Trainning instance, int applyForTrainingResult = 0, 
            bool checkIfStudentAppliedResult = false)
            : base(instance)
        {
            fakeInstance = instance; applyForTraining = applyForTrainingResult;
            checkIfStudentApplied = checkIfStudentAppliedResult;
        }
        public int ApplyForTraining(string stdId, int trainingId)
        {
            return applyForTraining;
        }

        public bool CheckIfStudentApplied(string studentUserName, int trainingId)
        {
            return checkIfStudentApplied;
        }
    }
}
