using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Extensions;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
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
        private readonly IGenericRepository<HrCompany> _comRepo;
        private readonly INotyfService _notyfService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IGenericRepository<Student> stuRepo,
            IGenericRepository<Supervisor> supRepo,
            SignInManager<IdentityUser> signInManager,
            IGenericRepository<Trainning> trainRepo,
            INotyfService notyfService,
            IGenericRepository<HrCompany> comRepo)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
            _stuRepo = stuRepo;
            _supRepo = supRepo;
            _signInManager = signInManager;
            _trainRepo = trainRepo;
            _notyfService = notyfService;
            _comRepo = comRepo;
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
                _notyfService.Success("Student created successfully");
                return View();
            }
            else
            {
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
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
                _notyfService.Success("Supervisor created successfully");
                return View();
            }
            else
            {
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
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
                ViewBag.LoginFailed = "Invalid details";
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
                ViewBag.LoginFailed = "Invalid details";
                return View(model);
            }
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("student/edit")]
        public async Task<ActionResult> EditStudent()
        {
            var logedInStudent = (Student)await _userManager.GetUserAsync(User);
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
                    _notyfService.Success("Account updated successfully");
                    return View();
                }
                else
                {
                    foreach(var er in result.Errors)
                    {
                        ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
                    }
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
            var logedInSupervisor = await _userManager.GetUserAsync(User);
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
                    _notyfService.Success("Account updated successfully");
                    return View();
                }
                else
                {
                    foreach (var er in result.Errors)
                    {
                        ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
                    }
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
            if (user == null) return RedirectToAction(nameof(Login));

            var result = await _userManager.ChangePasswordAsync(user,
                model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                _notyfService.Success("Password changed successfully");
                return View();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
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
                _notyfService.Success("Company created successfully");
                return View();
            }
            else
            {
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
                }
                return View(model);
            }
        }

        

        [HttpGet("company/edit")]
        public async Task<ActionResult> EditCompany()
        {
            var logedInHrCompany = await _userManager.GetUserAsync(User);
            if (logedInHrCompany == null) NotFound();
            var model = _mapper.Map<EditHrCompanyVM>(logedInHrCompany);
            return View(model);
        }

        [HttpPost("company/edit")]
        public async Task<ActionResult> EditCompany(EditHrCompanyVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await UpdateCompanyFromModelAsync(model);
                if (result.Succeeded)
                {
                    _notyfService.Success("Account updated successfully");
                    return View();
                }
                else
                {
                    foreach (var er in result.Errors)
                    {
                        ModelState.AddModelError("", ReturnEmailInsteadOfUsername(er.Description));
                    }
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet("my-trainings")]
        public async Task<IActionResult> GetTrainingsForCompany()
        {
            var loggedInCompanyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainings = await _trainRepo.ListAsync(t => t.CompanyId == loggedInCompanyId, 
                new string[] { 
                    Includes.Department.ToString(),
                    Includes.TrainingType.ToString()
                });
            var model = _mapper.Map<List<TrainingVM>>(trainings);
            return View(model);
        }

        [HttpGet("company-profile")]
        public async Task<IActionResult> CompanyProfile([FromQuery]string id)
        {
            if(string.IsNullOrEmpty(id)) id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var company = await _comRepo.GetAsync(c => c.Id == id, new string[] { Includes.CompanySize.ToString() });
            if (company == null) return NotFound();
            return View(_mapper.Map<CompanyVM>(company));
            
        }

        private async Task<IdentityResult> UpdateStudentFromModelAsync(EditStudentProfileVM model)
        {
            var student = (Student)await _userManager.FindByIdAsync(model.Id);

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.UserName = model.Email;
            student.PhoneNumber = model.PhoneNumber;
            return await _userManager.UpdateAsync(student);
        }

        private async Task<IdentityResult> UpdateSupervisorFromModelAsync(EditSupervisorProfileVM model)
        {
            var supervisor = (Supervisor)await _userManager.FindByIdAsync(model.Id);

            supervisor.FirstName = model.FirstName;
            supervisor.LastName = model.LastName;
            supervisor.Email = model.Email;
            supervisor.UserName = model.Email;
            supervisor.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(supervisor);
        }
        private async Task<IdentityResult> UpdateCompanyFromModelAsync(EditHrCompanyVM model)
        {
            var hrCompany = (HrCompany)await _userManager.FindByIdAsync(model.Id);

            hrCompany.Name = model.Name;
            hrCompany.PhoneNumber = model.PhoneNumber;
            hrCompany.City = model.City;
            hrCompany.Country = model.Country;
            hrCompany.Industry = model.Industry;
            hrCompany.Specialities = model.Specialities;
            hrCompany.Email = model.Email;
            hrCompany.UserName = model.Email;
            hrCompany.FoundedAt = model.FoundedAt;
            hrCompany.CompanyWebsite = model.CompanyWebsite;
            hrCompany.CompanySizeId = model.CompanySizeId;
            hrCompany.AboutCompany = model.AboutCompany;

            return await _userManager.UpdateAsync(hrCompany);
        }

        private static string ReturnEmailInsteadOfUsername(string text)
        {
            return text.Contains("Username") ? text.Replace("Username", "Email") : text;
        }

    }
}
