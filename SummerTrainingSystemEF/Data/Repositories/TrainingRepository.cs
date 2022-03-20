using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Data.Repositories
{
    public class TrainingRepository : GenericRepository<Trainning>, ITrainingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<int> ApplyForTraining(Student student, Trainning training)
        {
            training.Students.Add(student);
            training.ApplicantsCount += 1;
            student.Trainnings.Add(training);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfStudentApplied(string studentUserName, int trainingId)
        {
            var training = _context.Trainnings
                .Where(t => t.Id == trainingId)
                .Include(t => t.Students)
                .FirstOrDefault();

            var student = (Student)await _userManager.FindByNameAsync(studentUserName);

            return training.Students.Find(s => s.Id == student.Id) != null;
        }
    }
}
