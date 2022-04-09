using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Linq;

namespace SummerTrainingSystemEF.Data.Repositories
{
    public class TrainingRepository : GenericRepository<Trainning>, ITrainingRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public int ApplyForTraining(string stdId, int trainingId)
        {
            var studentId = new SqlParameter("@studentId", stdId);
            var trId = new SqlParameter("@trId", trainingId);
            try
            {
                return _context.Database.ExecuteSqlInterpolated($"exec spApplyForTraining {studentId}, {trId}");
            }
            catch
            {
                return 0;
            }
        }

        public bool CheckIfStudentApplied(string studentUserName, int trainingId)
        {
            var student = _context.Students
                .Where(t => t.UserName == studentUserName)
                .Include(t => t.Trainnings)
                .FirstOrDefault();

            return student.Trainnings.Find(s => s.Id == trainingId) != null;
        }
    }
}
