using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
using SummerTrainingSystemCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotyfService _notyfService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork,
            SignInManager<IdentityUser> signInManager,
            INotyfService notyfService,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _mapper = mapper;
            _signInManager = signInManager;
            _notyfService = notyfService;
            _env = env;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("student/new")]
        public IActionResult CreateStudentAccount()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("student/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentAccount(SaveStudentAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // upload the profile photo
            if (model.ProfilePicture != null)
            {
                string uploadsFolder = "uploads/";
                model.ProfilePictureUrl = await UploadFile(uploadsFolder, model.ProfilePicture);
            }
            var student = _mapper.Map<Student>(model);
            var result = await _accountService.CreateStudentAccountAsync(student, model.Password);
            if (result.Succeeded)
            {
                _notyfService.Success("Student created successfully");
                return RedirectToAction("GetAllStudents", "Users");
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

        [Authorize(Roles = "Admin")]
        [HttpGet("supervisor/new")]
        public IActionResult CreateSupervisorAccount()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("supervisor/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSupervisorAccount(SaveSupervisorAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // upload the profile photo
            if (model.ProfilePicture != null)
            {
                string uploadsFolder = "uploads/";
                model.ProfilePictureUrl = await UploadFile(uploadsFolder, model.ProfilePicture);
            }
            var supervisor = _mapper.Map<Supervisor>(model);
            var result = await _accountService.CreateSupervisorAccountAsync(supervisor, model.Password);
            if (result.Succeeded)
            {
                _notyfService.Success("Supervisor created successfully");
                return RedirectToAction("GetAllSupervisors", "Users");
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
        public IActionResult LoginAsCompany([FromQuery] string ReturnUrl)
        {
            var model = new LoginAsCompanyVM
            {
                ReturnUrl = ReturnUrl,
                Email = string.Empty,
                Password = string.Empty
            };
            return View(model);
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Student")]
        [HttpGet("student/edit")]
        public async Task<ActionResult> EditStudent()
        {
            var logedInStudent = (Student)await _userManager.GetUserAsync(User);
            if (logedInStudent == null) NotFound();
            return View(_mapper.Map<EditStudentProfileVM>(logedInStudent));
        }

        [Authorize(Roles = "Student")]
        [HttpPost("student/edit")]
        public async Task<ActionResult> EditStudent(EditStudentProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await UpdateStudentFromModelAsync(model);
                if (result.Succeeded)
                {
                    _notyfService.Success("Account updated successfully");
                    return RedirectToAction(nameof(StudentProfile));
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

        [Authorize(Roles = "Supervisor")]
        [HttpGet("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor()
        {
            var logedInSupervisor = await _userManager.GetUserAsync(User);
            if (logedInSupervisor == null) NotFound();
            var model = _mapper.Map<EditSupervisorProfileVM>(logedInSupervisor);
            return View(model);
        }

        [Authorize(Roles = "Supervisor")]
        [HttpPost("supervisor/edit")]
        public async Task<ActionResult> EditSupervisor(EditSupervisorProfileVM model)
        {
            if (ModelState.IsValid)
            {

                var result = await UpdateSupervisorFromModelAsync(model);
                if (result.Succeeded)
                {
                    _notyfService.Success("Account updated successfully");
                    return RedirectToAction(nameof(SupervisorProfile));
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

        [Authorize]
        [HttpGet("reset-password")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("company/new")]
        public IActionResult CreateCompanyAccount()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("company/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompanyAccount(SaveCompanyAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // upload the profile photo
            if (model.ProfilePicture != null)
            {
                string uploadsFolder = "uploads/";
                model.ProfilePictureUrl = await UploadFile(uploadsFolder, model.ProfilePicture);
            }
            var company = _mapper.Map<HrCompany>(model);
            var result = await _accountService.CreateCompanyAccountAsync(company, model.Password);
            if (result.Succeeded)
            {
                _notyfService.Success("Company created successfully");
                return RedirectToAction("GetAllCompanies", "Users");
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


        [Authorize(Roles = "Company")]
        [HttpGet("company/edit")]
        public async Task<ActionResult> EditCompany()
        {
            var logedInHrCompany = await _userManager.GetUserAsync(User);
            if (logedInHrCompany == null) NotFound();
            var model = _mapper.Map<EditHrCompanyVM>(logedInHrCompany);
            return View(model);
        }

        [Authorize(Roles = "Company")]
        [HttpPost("company/edit")]
        public async Task<ActionResult> EditCompany(EditHrCompanyVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await UpdateCompanyFromModelAsync(model);
                if (result.Succeeded)
                {
                    _notyfService.Success("Account updated successfully");
                    return RedirectToAction(nameof(CompanyProfile));
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

        [Authorize(Roles = "Company")]
        [HttpGet("company/trainings")]
        public async Task<IActionResult> GetTrainingsForCompany()
        {
            var loggedInCompanyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainings = await _unitOfWork.GenericRepository<Trainning>().ListAsync(t => t.CompanyId == loggedInCompanyId, 
                source => source.Include(s => s.TrainingType));

            var model = _mapper.Map<List<TrainingVM>>(trainings);
            return View(model);
        }

        [Authorize]
        [HttpGet("company-profile")]
        public async Task<IActionResult> CompanyProfile([FromQuery]string id)
        {
            if(string.IsNullOrEmpty(id)) id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var company = await _unitOfWork.GenericRepository<HrCompany>()
                .GetAsync(c => c.Id == id, source => source
                .Include(s => s.CompanySize)
                .Include(s  => s.Comments).ThenInclude(s => s.Student));
            if (company == null) return NotFound();
            if(company.Comments.Count > 1)
            {
                company.Comments = company.Comments.OrderByDescending(c => c.CreateAt).ToList();
            }
            return View(_mapper.Map<CompanyVM>(company));
            
        }

        [Authorize]
        [HttpGet("student-profile")]
        public async Task<IActionResult> StudentProfile()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await _unitOfWork.GenericRepository<Student>().GetAsync(s => s.Id == id, source => 
            source.Include(s => s.Department));
            if (student == null) return NotFound();
            return View(_mapper.Map<StudentVM>(student));
        }

        [Authorize(Roles = "Student")]
        [HttpGet("my-supervisors")]
        public async Task<IActionResult> MySupervisors()
        {
            var student = (Student)await _userManager.GetUserAsync(User);
            var supervisors = await _unitOfWork.GenericRepository<Supervisor>()
                .ListAsync(s => s.DepartmentId == student.DepartmentId, 
                source => source.Include(s => s.Department));
            return View(_mapper.Map<List<SupervisorVM>>(supervisors));
        }

        [Authorize(Roles = "Supervisor")]
        [HttpGet("my-students")]
        public async Task<IActionResult> MyStudents()
        {
            var supervisor = (Supervisor)await _userManager.GetUserAsync(User);
            var students = await _unitOfWork.GenericRepository<Student>()
                .ListAsync(s => s.DepartmentId == supervisor.DepartmentId,
                source => source.Include(s => s.Department));
            return View(_mapper.Map<List<StudentVM>>(students));
        }

        [Authorize]
        [HttpGet("supervisor-profile")]
        public async Task<IActionResult> SupervisorProfile()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var supervisor = await _unitOfWork.GenericRepository<Supervisor>().GetAsync(s => s.Id == id, 
                source => source.Include(s => s.Department));
            if (supervisor == null) return NotFound();
            return View(_mapper.Map<SupervisorVM>(supervisor));
        }

        [Authorize(Roles ="Student")]
        [HttpGet("student/trainings")]
        public async Task<IActionResult> GetTrainningForStudent()
        {
            var logedInStudent = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(logedInStudent == null) return NotFound();
            var student = await _unitOfWork.GenericRepository<Student>().GetAsync(s => s.Id == logedInStudent, source => 
            source.Include(s => s.Trainnings)
            .ThenInclude(s => s.TrainingType));
            var model = _mapper.Map<StudentVM>(student);

            return View(model);
        }

        [Authorize]
        [HttpPost("update-photo")]
        public async Task<IActionResult> UpdateProfilePhoto(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, Roles.Supervisor.ToString()))
            {
                var super = (Supervisor)user;
                if(!string.IsNullOrEmpty(super.ProfilePictureUrl))
                {
                    DeleteFile("uploads/", super.ProfilePictureUrl);
                }
                super.ProfilePictureUrl = await UploadFile("uploads/", file);
                _unitOfWork.GenericRepository<Supervisor>().Update(super);
                var result = await _unitOfWork.Complete();
                if (result > 0) return Ok();
            }
            if (await _userManager.IsInRoleAsync(user, Roles.Student.ToString()))
            {
                var student = (Student)user;
                if (!string.IsNullOrEmpty(student.ProfilePictureUrl))
                {
                    DeleteFile("uploads/", student.ProfilePictureUrl);
                }
                student.ProfilePictureUrl = await UploadFile("uploads/", file);
                _unitOfWork.GenericRepository<Student>().Update(student);
                var result = await _unitOfWork.Complete();
                if (result > 0) return Ok();
            }
            if (await _userManager.IsInRoleAsync(user, Roles.Company.ToString()))
            {
                var company = (HrCompany)user;
                if (!string.IsNullOrEmpty(company.ProfilePictureUrl))
                {
                    DeleteFile("uploads/", company.ProfilePictureUrl);
                }
                company.ProfilePictureUrl = await UploadFile("uploads/", file);
                _unitOfWork.GenericRepository<HrCompany>().Update(company);
                var result = await _unitOfWork.Complete();
                if (result > 0) return Ok();
            }
            
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            if (await _userManager.IsInRoleAsync(user, Roles.Student.ToString()))
            {
                // check if student has reviews
                var comments = await _unitOfWork.GenericRepository<Comment>().ListAsync(s => s.StudentId == id);
                if (comments.Count > 0)
                {
                    // update applicants count for those trainings
                    foreach (var c in comments)
                    {
                        _unitOfWork.GenericRepository<Comment>().Delete(c);
                    }
                }
                // check if student applied for trainings
                var student = await _unitOfWork.GenericRepository<Student>().GetAsync(s => s.Id == id, 
                    source => source.Include(s => s.Trainnings));
                if (student.Trainnings.Count > 0)
                {
                    // update applicants count for those trainings
                    foreach (var tr in student.Trainnings)
                    {
                        tr.ApplicantsCount -= 1;
                    }
                }
            }
            else if(await _userManager.IsInRoleAsync(user, Roles.Company.ToString()))
            {
                // check if company has reviews
                var comments = await _unitOfWork.GenericRepository<Comment>().ListAsync(s => s.HrCompanyId == id);
                if (comments.Count > 0)
                {
                    // update applicants count for those trainings
                    foreach (var c in comments)
                    {
                        _unitOfWork.GenericRepository<Comment>().Delete(c);
                    }
                }
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return Ok();
            return BadRequest();
        }

        [Route("IsUniversityIdInUse")]
        public JsonResult IsUniversityIdInUse([FromQuery] int UniversityId)
        {
            if (_accountService.CheckIsUniversityIdExists(UniversityId))
            {
                return Json($"University Id {UniversityId} is in use.");
            }
            else
            {
                return Json(true);
            }
        }

        [Route("IsEmailInUse")]
        public JsonResult IsEmailInUse([FromQuery] string Email)
        {
            if (_accountService.CheckIsEmailExists(Email))
            {
                return Json($"Email {Email} is in use.");
            }
            else
            {
                return Json(true);
            }
        }
        private void DeleteFile(string filePath, string fileUrl)
        {
            string fullFilePath = filePath + fileUrl;
            string FullPathOnServer = Path.Combine(_env.WebRootPath, fullFilePath);
            if ((System.IO.File.Exists(FullPathOnServer)))
            {
                System.IO.File.Delete(FullPathOnServer);
            }
        }

        private async Task<string> UploadFile(string filePath, IFormFile file)
        {

            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string fullPhotoPath = filePath + fileName;
            string FullPathOnServer = Path.Combine(_env.WebRootPath, fullPhotoPath);
            using (FileStream fileStream = new(FullPathOnServer, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
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
