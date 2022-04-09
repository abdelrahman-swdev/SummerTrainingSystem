using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("departments")]
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Department> _depRepo;

        public DepartmentsController(IMapper mapper,
            INotyfService notyfService,
            IUnitOfWork unitOfWork)
        {
            //_depRepo = depRepo;
            _mapper = mapper;
            _notyfService = notyfService;
            _unitOfWork = unitOfWork;
            _depRepo = _unitOfWork.GenericRepository<Department>();
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
        public async Task<IActionResult> CreateDepartment(CreateDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                _depRepo.Add(_mapper.Map<Department>(model));
                var result = await _unitOfWork.Complete();
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
                _depRepo.Delete(await _depRepo.GetByIdAsync(id));
                return await _unitOfWork.Complete() == 0 ? BadRequest() : Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
