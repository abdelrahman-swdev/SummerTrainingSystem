using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("student/new")]
        public IActionResult CreateStudentAccount()
        {
            return View();
        }

        [HttpPost("student/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentAccount(SaveStudentAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var student = _mapper.Map<Student>(model);
            var result = await _accountService.CreateStudentAccountAsync(student, model.Password);
            if (result.Succeeded)
            {
                ViewBag.StudentCreated = "Student Created Succefully";
                return View();
            }
            else
            {
                ViewBag.StudentNotCreated = "Error, Student Did Not Created";
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                }
                return View(model);
            }
        }

        [HttpGet("supervisor/new")]
        public IActionResult CreateSupervisorAccount()
        {
            return View();
        }

        [HttpPost("supervisor/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSupervisorAccount(SaveSupervisorAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var supervisor = _mapper.Map<Supervisor>(model);
            var result = await _accountService.CreateSupervisorAccountAsync(supervisor, model.Password);
            if (result.Succeeded)
            {
                ViewBag.SupervisorCreated = "Supervisor Created Succefully";
                return View();
            }
            else
            {
                ViewBag.SupervisorNotCreated = "Error, Supervisor Did Not Created";
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                }
                return View(model);
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.LoginAsync(model.UniversityId, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginFailed = "Error, invalid details";
                return View(model);
            }
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
