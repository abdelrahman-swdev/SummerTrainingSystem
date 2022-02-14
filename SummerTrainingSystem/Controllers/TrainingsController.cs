using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Data;

namespace SummerTrainingSystem.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingsController
        public ActionResult Index()
        {
            return View(_context.Trainnings.ToList());
        }

        // GET: TrainingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
