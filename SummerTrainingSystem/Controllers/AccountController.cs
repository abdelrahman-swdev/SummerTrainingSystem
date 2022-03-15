using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
        private readonly IGenericRepository<Trainning> _trainRepo;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IGenericRepository<Student> stuRepo,
            IGenericRepository<Supervisor> supRepo,
            SignInManager<IdentityUser> signInManager,
            IGenericRepository<Trainning> trainRepo)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
            _stuRepo = stuRepo;
            _supRepo = supRepo;
            _signInManager = signInManager;
            _trainRepo = trainRepo;
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

        [HttpGet("loginascompany")]
        public IActionResult LoginAsCompany()
        {
            return View();
        }

        [HttpPost("loginascompany")]
        public async Task<IActionResult> LoginAsCompany(LoginAsCompanyVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.LoginAsCompanyAsync(model.Email, model.Password);
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
            var logedInStudentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var logedInStudent = await _userManager.FindByIdAsync(logedInStudentId);
            if (logedInStudent == null) NotFound();
            return View(_mapper.Map<EditStudentProfileVM>(logedInStudent));
        }

        [HttpPost("student/edit")]
        public async Task<ActionResult> EditStudent(EditStudentProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await UpdateStudentFromModelAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.AccountUpdated = "Account Updated Succefully";
                    return View();
                }
                else
                {
                    ViewBag.AccountNotUpdated = "Error, Account Did Not Updated";
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor()
        {
            var logedInSupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var logedInSupervisor = await _userManager.FindByIdAsync(logedInSupervisorId);
            if (logedInSupervisor == null) NotFound();
            var model = _mapper.Map<EditSupervisorProfileVM>(logedInSupervisor);
            return View(model);
        }

        [HttpPost("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor(EditSupervisorProfileVM model)
        {
            if (ModelState.IsValid)
            {

                var result = await UpdateSupervisorFromModelAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.AccountUpdated = "Account Updated Succefully";
                    return View();
                }
                else
                {
                    ViewBag.AccountNotUpdated = "Error, Account Did Not Updated";
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet("reset-password")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
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

        [HttpGet("company/new")]
        public IActionResult CreateCompanyAccount()
        {
            return View();
        }

        [HttpPost("company/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompanyAccount(SaveCompanyAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var company = _mapper.Map<HrCompany>(model);
            var result = await _accountService.CreateCompanyAccountAsync(company, model.Password);
            if (result.Succeeded)
            {
                ViewBag.ComapnyCreated = "Company Created Succefully";
                return View();
            }
            else
            {
                ViewBag.ComapnyNotCreated = "Error, Company Did Not Created";
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                }
                return View(model);
            }
        }

        [HttpGet("my-trainings")]
        public async Task<IActionResult> GetTrainingsForCompany()
        {
            var loggedInCompanyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainings = await _trainRepo.ListAsync(t => t.CompanyId == loggedInCompanyId, 
                new string[] { "Department", "Company", "TrainingType" });
            var model = _mapper.Map<List<TrainingVM>>(trainings);
            return View(model);
        }

        private async Task<IdentityResult> UpdateStudentFromModelAsync(EditStudentProfileVM model)
        {
            var student = (Student)await _userManager.FindByIdAsync(model.Id);

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(student);
        }

        private async Task<IdentityResult> UpdateSupervisorFromModelAsync(EditSupervisorProfileVM model)
        {
            var supervisor = (Supervisor)await _userManager.FindByIdAsync(model.Id);

            supervisor.FirstName = model.FirstName;
            supervisor.LastName = model.LastName;
            supervisor.Email = model.Email;
            supervisor.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(supervisor);
        }



    }
}
