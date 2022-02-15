using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystem.Data;
using SummerTrainingSystem.Data.Entities;
using SummerTrainingSystem.Models;

namespace SummerTrainingSystem.Controllers
{
    [Route("trainings")]
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainingsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: TrainingsController
        [HttpGet("")]
        public ActionResult Index()
        {
            var trainings = _context.Trainnings.Include(t => t.Department).ToList();
            var model = _mapper.Map<IEnumerable<TrainingVM>>(trainings);
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
            var trainning = await _context.Trainnings
                .Where(t => t.Id == id)
                .Include(t => t.Department)
                .FirstOrDefaultAsync();

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
        public async Task<ActionResult> Create(TrainingVM model)
        {
            if (ModelState.IsValid)
            {
                Trainning tr = _mapper.Map<Trainning>(model);
                await _context.Trainnings.AddAsync(tr);
                await _context.SaveChangesAsync();

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
