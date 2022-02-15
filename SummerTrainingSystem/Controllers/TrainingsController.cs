using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystemEF.Data.Entities;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Interfaces;

namespace SummerTrainingSystem.Controllers
{
    [Route("trainings")]
    public class TrainingsController : Controller
    {
        private readonly IGenericRepository<Trainning> _trainRepo;
        private readonly IMapper _mapper;

        public TrainingsController(IGenericRepository<Trainning> trainRepo, IMapper mapper)
        {
            _trainRepo = trainRepo;
            _mapper = mapper;
        }

        // GET: TrainingsController
        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var trainings = await _trainRepo.ListAllAsync();
            var model = _mapper.Map<IReadOnlyList<TrainingVM>>(trainings);
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
            
            return View(_mapper.Map<TrainingVM>(trainning));
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
        public ActionResult Create(TrainingVM model)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
