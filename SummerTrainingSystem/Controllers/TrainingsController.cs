using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("trainings")]
    public class TrainingsController : Controller
    {
        private readonly IGenericRepository<Trainning> _trainRepo;
        private readonly IGenericRepository<Department> _depRepo;
        private readonly IMapper _mapper;

        public TrainingsController(IGenericRepository<Trainning> trainRepo, IGenericRepository<Department> depRepo, IMapper mapper)
        {
            _trainRepo = trainRepo;
            _depRepo = depRepo;
            _mapper = mapper;
        }

        // GET: trainings
        [HttpGet("")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: trainings/bydep or trainings/bydep?depid=5 to filter trainings by department
        [HttpGet("getall")]
        public async Task<ActionResult> GetTrainings([FromQuery] int depid, [FromQuery] string search)
        {
            // if search query is sent and department was selected
            if(!string.IsNullOrEmpty(search) && depid != 0)
            {
                return PartialView(await GetTrainingsByDepIdAndSearch(depid, search));
            }

            // if search query is sent
            if(!string.IsNullOrEmpty(search))
            {
                return PartialView(await GetTrainingsBySearch(search));
            }

            // if department id is sent
            if (depid != 0)
            {
                var result = await GetTrainingsByDepId(depid);
                if (result == null) return NotFound();
                return PartialView(result);
            }

            // all trainings (no search query was sent or department was selected)
            var trainings = await _trainRepo.ListAsync(t => true, new string[] { "Department", "Company", "TrainingType" });
            var model = _mapper.Map<IReadOnlyList<TrainingVM>>(trainings);
            return PartialView(model);
        }

        // GET: trainings/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Details([FromRoute] int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var trainning = await _trainRepo.GetAsync(t => t.Id == id, new string[] { "Department", "Company", "TrainingType" });

            if (trainning == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TrainingVM>(trainning));
        }

        // GET: trainings/new
        [HttpGet("new")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: trainings/new
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaveTrainingVM model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return _trainRepo.Add(_mapper.Map<Trainning>(model)) == 0 ? 
                    View(model) : RedirectToAction("GetTrainingsForCompany", "Account");
            }
            return View(model);
        }

        // GET: trainings/edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var tr = await _trainRepo.GetByIdAsync(id);
            if (tr == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<SaveTrainingVM>(tr));
        }

        // POST: trainings/edit/5
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SaveTrainingVM model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return NotFound();
                return _trainRepo.Update(_mapper.Map<Trainning>(model)) == 0 ? 
                    View(model) : RedirectToAction("GetTrainingsForCompany", "Account");
            }
            else
            {
                return View(model);
            }
        }

        // Delete: trainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return _trainRepo.Delete(await _trainRepo.GetByIdAsync(id)) == 0 ? BadRequest() : Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        // if search query is sent and department was selected
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsByDepIdAndSearch(int depid, string search)
        {
            var trainningsBySearchAndDep = await _trainRepo.ListAsync(t =>
                    t.DepartmentId == depid && (t.Title.Contains(search) || t.Description.Contains(search)),
                    new string[] { "Department", "Company", "TrainingType" }
                );
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsBySearchAndDep);
        }

        // trainings by specific department
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsByDepId(int depid)
        {
            var department = await _depRepo.GetByIdAsync(depid);
            if (department == null) return null;
            var trainningsByDepartment = await _trainRepo.ListAsync(t =>
                t.DepartmentId == department.Id, new string[] { "Department", "Company", "TrainingType" }
            );
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsByDepartment);
        }

        // trainings by search query
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsBySearch(string search)
        {
            var trainningsBySearch = await _trainRepo.ListAsync(t => t.Title.Contains(search) || t.Description.Contains(search),
                new string[] { "Department", "Company", "TrainingType" });
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsBySearch);
        }
    }
}
