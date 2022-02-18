using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystemCore.Entities;

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

        // GET: TrainingsController
        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var trainings = await _trainRepo.ListAllAsync();
            var model = _mapper.Map<IReadOnlyList<SaveTrainingVM>>(trainings);
            return View(model);
        }

        // GET: TrainingsController/Details/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Details([FromRoute] int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var trainning = await _trainRepo.GetByIdAsync(id);

            if (trainning == null)
            {
                return NotFound();
            }
            
            return View(_mapper.Map<SaveTrainingVM>(trainning));
        }

        // GET: TrainingsController/Create
        [HttpGet("new")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingsController/Create
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaveTrainingVM model)
        {
            if (ModelState.IsValid)
            {
                Trainning tr = _mapper.Map<Trainning>(model);
                _trainRepo.Add(tr);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: TrainingsController/Edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var tr = await _trainRepo.GetByIdAsync(id);
            if(tr == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<SaveTrainingVM>(tr));
        }

        // POST: TrainingsController/Edit/5
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SaveTrainingVM model)
        {
            if (ModelState.IsValid) 
            {
                if (id != model.Id) return NotFound();
                _trainRepo.Update(_mapper.Map<Trainning>(model));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: TrainingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
