using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("departments")]
    public class DepartmentsController : Controller
    {
        private readonly IGenericRepository<Department> _depRepo;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;

        public DepartmentsController(IGenericRepository<Department> depRepo,
            IMapper mapper,
            INotyfService notyfService)
        {
            _depRepo = depRepo;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _depRepo.ListAllAsync();
            return View(departments);
        }

        [HttpGet("new")]
        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult CreateDepartment(CreateDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var result = _depRepo.Add(_mapper.Map<Department>(model));
                if (result > 0)
                {
                    _notyfService.Success("Department created successfully");
                    return RedirectToAction("GetDepartments");
                }
            }
            return View(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                return _depRepo.Delete(await _depRepo.GetByIdAsync(id)) == 0 ? BadRequest() : Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
