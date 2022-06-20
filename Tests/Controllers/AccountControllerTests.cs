using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SummerTrainingSystem.Controllers;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tests.TestClasses;
using Xunit;

namespace Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task CreateStudentAccount_IvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await controller.CreateStudentAccount(new SaveStudentAccountVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveStudentAccountVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateStudentAccount_ValidModel_ResutlSucceeded()
        {
            //Arrange
            var newStudentAccountVM = new SaveStudentAccountVM();
            var student = new Student();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<Student>(newStudentAccountVM))
            .Returns(student);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateStudentAccountAsync(student, newStudentAccountVM.Password))
            .ReturnsAsync(IdentityResult.Success);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, mockRepo3.Object, null);

            //Act
            var result = await controller.CreateStudentAccount(newStudentAccountVM);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Users", redirectToActionResult.ControllerName);
            Assert.Equal("GetAllStudents", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task CreateStudentAccount_ValidModel_ResutlFailed()
        {
            //Arrange
            var newStudentAccountVM = new SaveStudentAccountVM();
            var student = new Student();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<Student>(newStudentAccountVM))
            .Returns(student);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateStudentAccountAsync(student, newStudentAccountVM.Password))
            .ReturnsAsync(new IdentityResult());
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, mockRepo3.Object, null);

            //Act
            var result = await controller.CreateStudentAccount(newStudentAccountVM);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveStudentAccountVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateSuperVisorAccount_IvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await controller.CreateSupervisorAccount(new SaveSupervisorAccountVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveSupervisorAccountVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateSpervisorAccount_ValidModel_ResutlSucceeded()
        {
            //Arrange
            var newSupervisorAccountVM = new SaveSupervisorAccountVM();
            var supervisor = new Supervisor();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<Supervisor>(newSupervisorAccountVM))
            .Returns(supervisor);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateSupervisorAccountAsync(supervisor, newSupervisorAccountVM.Password))
            .ReturnsAsync(IdentityResult.Success);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, mockRepo3.Object, null);

            //Act
            var result = await controller.CreateSupervisorAccount(newSupervisorAccountVM);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Users", redirectToActionResult.ControllerName);
            Assert.Equal("GetAllSupervisors", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task CreateSpervisorAccount_ValidModel_ResutlFailed()
        {
            //Arrange
            var newSupervisorAccountVM = new SaveSupervisorAccountVM();
            var supervisor = new Supervisor();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<Supervisor>(newSupervisorAccountVM))
            .Returns(supervisor);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateSupervisorAccountAsync(supervisor, newSupervisorAccountVM.Password))
            .ReturnsAsync(new IdentityResult());
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, mockRepo3.Object, null);

            //Act
            var result = await controller.CreateSupervisorAccount(newSupervisorAccountVM);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveSupervisorAccountVM>(viewResult.Model);
        }

        [Fact]
        public void LoginAsCompany_Get()
        {
            //Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);

            //Act
            var result = controller.LoginAsCompany("testURL");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LoginAsCompanyVM>(viewResult.Model);
        }

        [Fact]
        public async Task LoginAsCompany_Post_InvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = await controller.LoginAsCompany(new LoginAsCompanyVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LoginAsCompanyVM>(viewResult.Model);
        }

        [Fact]
        public async Task LoginAsCompany_Post_ResultSucceeded()
        {
            // Arrange
            var model = new LoginAsCompanyVM()
            {
                Email = "company_email@gmail.com",
                Password = "P@$$w0rd!"
            };
            var mockRepo = new Mock<IAccountService>();
            mockRepo.Setup(repo => repo.LoginAsCompanyAsync(model.Email, model.Password))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = new AccountController(mockRepo.Object, null, null, null, null, null, null);

            // Act
            var result = await controller.LoginAsCompany(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", viewResult.ControllerName);
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact]
        public async Task LoginAsCompany_Post_ResultFailed()
        {
            // Arrange
            var model = new LoginAsCompanyVM()
            {
                Email = "company_email@gmail.com",
                Password = "P@$$w0rd!"
            };
            var mockRepo = new Mock<IAccountService>();
            mockRepo.Setup(repo => repo.LoginAsCompanyAsync(model.Email, model.Password))
            .ReturnsAsync(new Microsoft.AspNetCore.Identity.SignInResult());
            var controller = new AccountController(mockRepo.Object, null, null, null, null, null, null);

            // Act
            var result = await controller.LoginAsCompany(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LoginAsCompanyVM>(viewResult.Model);
        }

        [Fact]
        public async Task Login_Post_InvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("Password", "Required");

            // Act
            var result = await controller.Login(new LoginVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LoginVM>(viewResult.Model);
        }

        [Fact]
        public async Task Login_Post_ResultSucceeded()
        {
            // Arrange
            var model = new LoginVM()
            {
                UniversityId = 5,
                Password = "P@$$w0rd!"
            };
            var mockRepo = new Mock<IAccountService>();
            mockRepo.Setup(repo => repo.LoginAsync(model.UniversityId, model.Password))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = new AccountController(mockRepo.Object, null, null, null, null, null, null);

            // Act
            var result = await controller.Login(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", viewResult.ControllerName);
            Assert.Equal("Index", viewResult.ActionName);
        }



        [Fact]
        public async Task Login_Post_ResultFailed()
        {
            // Arrange
            var model = new LoginVM()
            {
                UniversityId = 5,
                Password = "P@$$w0rd!"
            };
            var mockRepo = new Mock<IAccountService>();
            mockRepo.Setup(repo => repo.LoginAsync(model.UniversityId, model.Password))
            .ReturnsAsync(new Microsoft.AspNetCore.Identity.SignInResult());
            var controller = new AccountController(mockRepo.Object, null, null, null, null, null, null);

            // Act
            var result = await controller.Login(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LoginVM>(viewResult.Model);
        }

        [Fact]
        public async Task Logout()
        {
            // Arrange
            var mockRepo = new Mock<IAccountService>();
            mockRepo.Setup(repo => repo.LogoutAsync());
            var controller = new AccountController(mockRepo.Object, null, null, null, null, null, null);

            // Act
            var result = await controller.Logout();

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", viewResult.ControllerName);
            Assert.Equal("Index", viewResult.ActionName);
        }

        //[Fact]
        //public async Task EditStudent_NotFound()
        //{
        //    //Arrange
        //    var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        //    var mockUser = new FakeUserManager2(mockUserStore.Object);
        //    var controller = new AccountController(null, null, mockUser, null, null, null, null);

        //    //Act
        //    var result = await controller.EditStudent();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        [Fact]
        public async Task EditStudent_ViewResult()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager(mockUserStore.Object);
            var model = new Student();
            var model2 = new EditStudentProfileVM();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<EditStudentProfileVM>(model))
            .Returns(model2);
            var controller = new AccountController(null, mockRepo1.Object, mockUser, null, null, null, null);

            //Act
            var result = await controller.EditStudent();


            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditStudent_Post_InvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await controller.EditStudent(new EditStudentProfileVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditStudentProfileVM>(viewResult.Model);
        }

        [Fact]
        public async Task EditStudent_Post_ValidModel_ResultSucceeded()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager(mockUserStore.Object);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, null, mockRepo3.Object, null);
            var model = new EditStudentProfileVM();

            // Act
            var result = await controller.EditStudent(model);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("StudentProfile", redirectToAction.ActionName);
        }

        [Fact]
        public async Task EditStudent_Post_ValidModel_ResultFailed()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager2(mockUserStore.Object);
            var controller = new AccountController(null, null, mockUser, null, null, null, null);
            var model = new EditStudentProfileVM();

            // Act
            var result = await controller.EditStudent(model);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        //public async Task EditSupervisor_NotFound()
        //{
        //    //Arrange
        //    var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        //    var mockUser = new FakeUserManager2(mockUserStore.Object);
        //    var controller = new AccountController(null, null, mockUser, null, null, null, null);

        //    //Act
        //    var result = await controller.EditSupervisor();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        [Fact]
        public async Task EditSupervisor_ViewResult()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager(mockUserStore.Object);
            var model = new Supervisor();
            var model2 = new EditSupervisorProfileVM();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<EditSupervisorProfileVM>(model))
            .Returns(model2);
            var controller = new AccountController(null, mockRepo1.Object, mockUser, null, null, null, null);

            //Act
            var result = await controller.EditSupervisor();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditSupervisor_Post_InvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("FirstName", "Required");
            var model = new EditSupervisorProfileVM();

            // Act
            var result = await controller.EditSupervisor(model);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditSupervisorProfileVM>(viewResult.Model);
        }

        [Fact]
        public async Task EditSupervisor_Post_ValidModel_ResultSucceeded()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager3(mockUserStore.Object);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, null, mockRepo3.Object, null);
            var model = new EditSupervisorProfileVM();

            // Act
            var result = await controller.EditSupervisor(model);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SupervisorProfile", redirectToAction.ActionName);
        }

        [Fact]
        public async Task EditSupervisor_Post_ValidModel_ResultFailed()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager4(mockUserStore.Object);
            var controller = new AccountController(null, null, mockUser, null, null, null, null);
            var model = new EditSupervisorProfileVM();

            // Act
            var result = await controller.EditSupervisor(model);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ResetPassword_RedirectToAction()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager2(mockUserStore.Object);
            var controller = new AccountController(null, null, mockUser, null, null, null, null);

            //Act
            var result = await controller.ResetPassword(new ResetPasswordVM());

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToAction.ActionName);
        }

        [Fact]
        public async Task ResetPassword_ResultSucceeded()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager5(mockUserStore.Object);
            var mockSingin = new FakeSignInManager(mockUser);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, mockSingin, mockRepo3.Object, null);

            //Act
            var result = await controller.ResetPassword(new ResetPasswordVM() { NewPassword = "12345", ConfirmPassword = "12345", CurrentPassword = "12345" });

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ResetPassword_ResultFailed()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager3(mockUserStore.Object);
            var mockSingin = new FakeSignInManager(mockUser);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, mockSingin, mockRepo3.Object, null);

            //Act
            var result = await controller.ResetPassword(new ResetPasswordVM() { NewPassword = "12345", ConfirmPassword = "12345", CurrentPassword = "12345" });

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ResetPasswordVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateCompanyAccount_IvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("FirstName", "Required");
            var model = new SaveCompanyAccountVM();

            // Act
            var result = await controller.CreateCompanyAccount(model);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveCompanyAccountVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateCompanyAccount_ValidModel_ResutlSucceeded()
        {
            //Arrange
            var model = new SaveCompanyAccountVM();
            var hrCompany = new HrCompany();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<HrCompany>(model))
            .Returns(hrCompany);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateCompanyAccountAsync(hrCompany, model.Password))
            .ReturnsAsync(IdentityResult.Success);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, mockRepo3.Object, null);

            //Act
            var result = await controller.CreateCompanyAccount(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Users", redirectToActionResult.ControllerName);
            Assert.Equal("GetAllCompanies", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task CreateCompanyAccount_ValidModel_ResutlFailed()
        {
            //Arrange
            var model = new SaveCompanyAccountVM();
            var hrCompany = new HrCompany();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<HrCompany>(model))
            .Returns(hrCompany);
            var mockRepo2 = new Mock<IAccountService>();
            mockRepo2.Setup(repo => repo.CreateCompanyAccountAsync(hrCompany, model.Password))
            .ReturnsAsync(new IdentityResult());
            var controller = new AccountController(mockRepo2.Object, mockRepo1.Object, null, null,
                null, null, null);

            //Act
            var result = await controller.CreateCompanyAccount(model);

            //Assert
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveCompanyAccountVM>(viewResult.Model);
        }

        //[Fact]
        //public async Task EditCompany_NotFound()
        //{
        //    //Arrange
        //    var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        //    var mockUser = new FakeUserManager2(mockUserStore.Object);
        //    var controller = new AccountController(null, null, mockUser, null, null, null, null);

        //    //Act
        //    var result = await controller.EditSupervisor();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        [Fact]
        public async Task EditCompany_ViewResult()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager(mockUserStore.Object);
            var model = new HrCompany();
            var model2 = new EditHrCompanyVM();
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<EditHrCompanyVM>(model))
            .Returns(model2);
            var controller = new AccountController(null, mockRepo1.Object, mockUser, null, null, null, null);

            //Act
            var result = await controller.EditSupervisor();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditCompany_Post_InvalidModel()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null, null, null, null);
            controller.ModelState.AddModelError("Name", "Required");
            var model = new EditHrCompanyVM();

            // Act
            var result = await controller.EditCompany(model);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditHrCompanyVM>(viewResult.Model);
        }

        [Fact]
        public async Task EditCompany_Post_ValidModel_ResultSucceeded()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager6(mockUserStore.Object);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, null, mockRepo3.Object, null);
            var model = new EditHrCompanyVM();

            // Act
            var result = await controller.EditCompany(model);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CompanyProfile", redirectToAction.ActionName);
        }

        [Fact]
        public async Task EditCompany_Post_ValidModel_ResultFailed()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager7(mockUserStore.Object);
            var mockRepo3 = new Mock<INotyfService>();
            var controller = new AccountController(null, null, mockUser, null, null, mockRepo3.Object, null);
            var model = new EditHrCompanyVM();

            // Act
            var result = await controller.EditCompany(model);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditHrCompanyVM>(viewResult.Model);
        }

        [Fact]
        public async Task GetTrainingsForCompany()
        {
            //Arrange
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<Trainning>();
            mockRepo4.Setup(repo => repo.GenericRepository<Trainning>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<List<TrainingVM>>(fakeGR))
            .Returns(new List<TrainingVM>());
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.GetTrainingsForCompany();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CompanyProfile_ViewResult()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<HrCompany>(new HrCompany());
            var company = new HrCompany();
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            mockRepo4.Setup(repo => repo.GenericRepository<HrCompany>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<CompanyVM>(company)).Returns(new CompanyVM());
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.CompanyProfile("1");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CompanyProfile_NotFound()
        {
            //Arrange
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<HrCompany>();
            mockRepo4.Setup(repo => repo.GenericRepository<HrCompany>()).Returns(fakeGR);
            var controller = new AccountController(null, null, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.CompanyProfile("1");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task StudentProfile_NotFound()
        {
            //Arrange
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<Student>();
            mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            var controller = new AccountController(null, null, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.StudentProfile();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task StudentProfile_ViewResult()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Student>(new Student());
            var student = new Student();
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            mockRepo1.Setup(repo => repo.Map<StudentVM>(student)).Returns(new StudentVM());
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.StudentProfile();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task MySupervisors()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager(mockUserStore.Object);
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<Supervisor>();
            mockRepo4.Setup(repo => repo.GenericRepository<Supervisor>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            var controller = new AccountController(null, mockRepo1.Object, mockUser, mockRepo4.Object, null, null, null);

            //Act
            var result = await controller.MySupervisors();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task MyStudents()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var mockUser = new FakeUserManager8(mockUserStore.Object);
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<Student>();
            mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            var controller = new AccountController(null, mockRepo1.Object, mockUser, mockRepo4.Object, null, null, null);

            //Act
            var result = await controller.MyStudents();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task SupervisorProfile_NotFound()
        {
            //Arrange
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            var fakeGR = new FakeIGenericRepository<Supervisor>();
            mockRepo4.Setup(repo => repo.GenericRepository<Supervisor>()).Returns(fakeGR);
            var controller = new AccountController(null, null, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.SupervisorProfile();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task SupervisorProfile_ViewResult()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Supervisor>(new Supervisor());
            var supervisor = new Supervisor();
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            mockRepo4.Setup(repo => repo.GenericRepository<Supervisor>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.SupervisorProfile();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task GetTrainningForStudent_NotFound()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Student>(new Student());
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(new ClaimsPrincipal());
            var mockRepo4 = new Mock<IUnitOfWork>();
            mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.GetTrainningForStudent();

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetTrainningForStudent_ViewResult()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Student>(new Student());
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "John Doe"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var mockRepo = new Mock<HttpContext>();
            mockRepo.Setup(repo => repo.User).Returns(claimsPrincipal);
            var mockRepo4 = new Mock<IUnitOfWork>();
            mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            var mockRepo1 = new Mock<IMapper>();
            var controller = new AccountController(null, mockRepo1.Object, null, mockRepo4.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockRepo.Object;

            //Act
            var result = await controller.GetTrainningForStudent();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async Task UpdateProfilePhoto_NotFound()
        {
            //Arrange
            var mockUser = new FakeGenericUserManager("Not Found");
            var controller = new AccountController(null, null, mockUser, null, null, null, null);

            //Act
            var result = await controller.UpdateProfilePhoto(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAccount_NotFound()
        {
            //Arrange
            var mockUser = new FakeGenericUserManager();
            var controller = new AccountController(null, null, mockUser, null, null, null, null);

            //Act
            var result = await controller.DeleteAccount("1");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task DeleteAccount_OkResult()
        //{
        //    //Arrange
        //    var mockUser = new FakeGenericUserManager("Student", true, new Student());
        //    var mockRepo4 = new Mock<IUnitOfWork>();
        //    var fakeGR = new FakeIGenericRepository<Comment>(new Comment());
        //    var fakeGR1 = new FakeIGenericRepository<Student>(new Student());
        //    mockRepo4.Setup(repo => repo.GenericRepository<Comment>()).Returns(fakeGR);
        //    mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR1);
        //    var controller = new AccountController(null, null, mockUser, mockRepo4.Object, null, null, null);

        //    //Act
        //    var result = await controller.DeleteAccount("1");

        //    //Assert
        //    Assert.IsType<OkResult>(result);
        //}

        //[Fact]
        //public async Task DeleteAccount_BadRequest()
        //{
        //    //Arrange
        //    var mockUser = new FakeGenericUserManager("Student", false, new Student());
        //    var mockRepo4 = new Mock<IUnitOfWork>();
        //    var fakeGR = new FakeIGenericRepository<Comment>(new Comment());
        //    var fakeGR1 = new FakeIGenericRepository<Student>(new Student());
        //    mockRepo4.Setup(repo => repo.GenericRepository<Comment>()).Returns(fakeGR);
        //    mockRepo4.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR1);
        //    var controller = new AccountController(null, null, mockUser, mockRepo4.Object, null, null, null);

        //    //Act
        //    var result = await controller.DeleteAccount("1");

        //    //Assert
        //    Assert.IsType<BadRequestResult>(result);
        //}

        [Fact]
        public async Task IsUniversityIdInUse_True()
        {
            //Arrange
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(repo => repo.CheckIsUniversityIdExists(1)).Returns(true);
            var controller = new AccountController(mockAccountService.Object, null, null, null, null, null, null);

            //Act
            var result = controller.IsUniversityIdInUse(1);

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal($"University Id {1} is in use.", jsonResult.Value);
        }


        [Fact]
        public async Task IsUniversityIdInUse_False()
        {
            //Arrange
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(repo => repo.CheckIsUniversityIdExists(1)).Returns(false);
            var controller = new AccountController(mockAccountService.Object, null, null, null, null, null, null);

            //Act
            var result = controller.IsUniversityIdInUse(1);

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(true, jsonResult.Value);
        }

        [Fact]
        public async Task IsEmailInUse_True()
        {
            //Arrange
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(repo => repo.CheckIsEmailExists("ahmed@gmail.com")).Returns(true);
            var controller = new AccountController(mockAccountService.Object, null, null, null, null, null, null);

            //Act
            var result = controller.IsEmailInUse("ahmed@gmail.com");

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal($"Email ahmed@gmail.com is in use.", jsonResult.Value);
        }


        [Fact]
        public async Task IsEmailInUse_False()
        {
            //Arrange
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(repo => repo.CheckIsEmailExists("ahmed@gmail.com")).Returns(false);
            var controller = new AccountController(mockAccountService.Object, null, null, null, null, null, null);

            //Act
            var result = controller.IsEmailInUse("ahmed@gmail.com");

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(true, jsonResult.Value);
        }
    }
}