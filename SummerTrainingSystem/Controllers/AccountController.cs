using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SummerTrainingSystem.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGenericRepository<Student> _stuRepo;
        private readonly IGenericRepository<Supervisor> _supRepo;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IGenericRepository<Student> stuRepo,
            IGenericRepository<Supervisor> supRepo,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
            _stuRepo = stuRepo;
            _supRepo = supRepo;
            _signInManager = signInManager;
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
        [HttpGet("student/edit")]
        public async Task<ActionResult> EditStudent()
        {
            var logedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var logedInStudent = await _userManager.FindByIdAsync(logedInUserId);
            if (logedInStudent == null) NotFound();
            return View(_mapper.Map<EditStudentProfileVM>(logedInStudent));
        }

        [HttpPost("student/edit")]
        public async Task<ActionResult> EditStudent(EditStudentProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var student = await _stuRepo.GetByStringIdAsync(model.Id);
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.PhoneNumber = model.PhoneNumber;
                _stuRepo.Update(student);
                return RedirectToAction("EditStudentProfile");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor()
        {
            var logedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var logedInSupervisor = await _userManager.FindByIdAsync(logedInUserId);
            if (logedInSupervisor == null) NotFound();
            return View(_mapper.Map<EditSupervisorProfileVM>(logedInSupervisor));
        }
        [HttpPost("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor(EditSupervisorProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var supervisor = await _supRepo.GetByStringIdAsync(model.Id);
                supervisor.FirstName = model.FirstName;
                supervisor.LastName = model.LastName;
                supervisor.Email = model.Email;
                supervisor.PhoneNumber = model.PhoneNumber;
                _supRepo.Update(supervisor);
                return RedirectToAction("EditSupervisorProfile");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet("resetPassword")]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var result = await _userManager.ChangePasswordAsync(user,
                model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.PasswordChanged = "Password Changed Succefully";
                return View();
            }
            else
            {
                ViewBag.PasswordNotChanged = "Error, Password Did Not Changed";
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }
    }
}
