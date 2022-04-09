using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("trainings")]
    [Authorize]
    public class TrainingsController : Controller
    {
        private readonly ITrainingRepository _trainRepo;
        private readonly IGenericRepository<Department> _depRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingsController(IUnitOfWork unitOfWork,
            IMapper mapper,
            INotyfService notyfService,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notyfService = notyfService;
            _userManager = userManager;
            _trainRepo = _unitOfWork.TrainningRepository();
            _depRepo = _unitOfWork.GenericRepository<Department>();
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
            var trainings = await _trainRepo.ListAsync(t => true, source => source
            .Include(a => a.Department)
            .Include(a => a.TrainingType)
            .Include(a => a.Company));

            var model = _mapper.Map<IReadOnlyList<TrainingVM>>(trainings);
            return PartialView(model);
        }

        // GET: trainings/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Details([FromRoute] int id)
        {
            var trainning = await _trainRepo.GetAsync(t => t.Id == id, source => 
            source
            .Include(s => s.Company)
            .Include(s => s.TrainingType)
            .Include(s => s.Students));

            if (trainning == null) return NotFound();
            return View(_mapper.Map<TrainingVM>(trainning));
        }

        // GET: trainings/new
        [Authorize(Roles = "Admin,Company")]
        [HttpGet("new")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: trainings/new
        [Authorize(Roles = "Admin,Company")]
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveTrainingVM model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _trainRepo.Add(_mapper.Map<Trainning>(model));
                var result = await _unitOfWork.Complete();
                if(result > 0)
                {
                    _notyfService.Success("Training created successfully");
                    return RedirectToAction("GetTrainingsForCompany", "Account");
                }
            }
            return View(model);
        }

        // GET: trainings/edit/5
        [Authorize(Roles = "Admin,Company")]
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
        [Authorize(Roles = "Admin,Company")]
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SaveTrainingVM model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return NotFound();
                _trainRepo.Update(_mapper.Map<Trainning>(model));
                var result = await _unitOfWork.Complete();
                if (result != 0)
                {
                    _notyfService.Success("Training updated successfully");
                    return RedirectToAction("GetTrainingsForCompany","Account");
                }
                else
                {
                    ViewBag.TrainingNotUpdated = "Error, Training Did Not Updated";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // Delete: trainings/5
        [Authorize(Roles = "Admin,Company")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _trainRepo.Delete(await _trainRepo.GetByIdAsync(id));
                return await _unitOfWork.Complete() == 0 ? BadRequest() : Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // apply for training
        [Authorize(Roles = "Student")]
        [HttpGet("{trid}/apply")]
        public async Task<IActionResult> ApplyForTraining(int trid)
        {
            var loggedInStudentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(loggedInStudentId == null || await _trainRepo.GetByIdAsync(trid) == null) return NotFound();

            int result = _trainRepo.ApplyForTraining(loggedInStudentId, trid);

            if(result > 0)
            {
                _notyfService.Success("Applied Successfully.");
                return RedirectToAction(nameof(Details), new {id = trid});
            }

            _notyfService.Error("Applying did not complete.");
            return RedirectToAction(nameof(Details), new { id = trid });
        }

        // Get Applications For Training
        [Authorize(Roles = "Admin,Company")]
        [HttpGet("{trid}/applications")]
        public async Task<IActionResult> GetApplicationsForTraining(int trid)
        {
            var tr = await _trainRepo.GetAsync(t => t.Id == trid, source => source.Include(s => s.Students).ThenInclude(s => s.Department));
            if(tr == null) return NotFound();
            var model = _mapper.Map<TrainingVM>(tr);
            return View(model);
        }


        // if search query is sent and department was selected
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsByDepIdAndSearch(int depid, string search)
        {
            var trainningsBySearchAndDep = await _trainRepo.ListAsync(t =>
                    t.DepartmentId == depid && (t.Title.Contains(search) || t.Description.Contains(search)), source => source
                    .Include(s => s.Company)
                    .Include(s => s.Department)
                    .Include(s => s.TrainingType)
                );
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsBySearchAndDep);
        }

        // trainings by specific department
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsByDepId(int depid)
        {
            var department = await _depRepo.GetByIdAsync(depid);
            if (department == null) return null;
            var trainningsByDepartment = await _trainRepo.ListAsync(t =>
                t.DepartmentId == department.Id, source => source
                    .Include(s => s.Company)
                    .Include(s => s.Department)
                    .Include(s => s.TrainingType)
                );
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsByDepartment);
        }

        // trainings by search query
        private async Task<IReadOnlyList<TrainingVM>> GetTrainingsBySearch(string search)
        {
            var trainningsBySearch = await _trainRepo.ListAsync(t => 
            t.Title.Contains(search) || 
            t.Description.Contains(search), source => source
                    .Include(s => s.Company)
                    .Include(s => s.Department)
                    .Include(s => s.TrainingType)
                );
            return _mapper.Map<IReadOnlyList<TrainingVM>>(trainningsBySearch);
        }
    }
}
