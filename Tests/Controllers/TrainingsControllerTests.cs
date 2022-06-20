using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tests.TestClasses;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystem.Controllers;

namespace Tests.Controllers
{
    public class TrainingsControllerTests
    {
        Mock<IMapper> mockMapper = new Mock<IMapper>();
        Mock<IUnitOfWork> mockUnitOfWrok = new Mock<IUnitOfWork>();
        Mock<INotyfService> mockNotyfService = new Mock<INotyfService>();
        Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        FakeTrainingRepository fakeTR = new FakeTrainingRepository();
        FakeTrainingRepository fakeTR1 = new FakeTrainingRepository(new Trainning());
        FakeIGenericRepository<Department> fakeGR = new FakeIGenericRepository<Department>();

        [Fact]
        public async Task GetTrainings_WithIdAndSearch()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<IReadOnlyList<TrainingVM>>(new Trainning())).Returns(new List<TrainingVM>());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.GetTrainings(1, "Searching for department");

            //Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(partialViewResult.Model);
        }

        [Fact]
        public async Task GetTrainings_WithSearch()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<IReadOnlyList<TrainingVM>>(new Trainning())).Returns(new List<TrainingVM>());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.GetTrainings(0, "Searching for department");

            //Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(partialViewResult.Model);
        }

        [Fact]
        public async Task GetTrainings_WithId_NotFound()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<IReadOnlyList<TrainingVM>>(new Trainning())).Returns(new List<TrainingVM>());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.GetTrainings(1, null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetTrainings_WithoutIdOrDepartment()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<IReadOnlyList<TrainingVM>>(new Trainning())).Returns(new List<TrainingVM>());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.GetTrainings(0, null);

            //Assert
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public async Task Details_NotFound()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<TrainingVM>(new Trainning())).Returns(new TrainingVM());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.Details(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ViewResult()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<TrainingVM>(new Trainning())).Returns(new TrainingVM());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR1);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.Details(0);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_InvalidModel()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);
            controller.ModelState.AddModelError("Title", "Title is required");
            var model = new SaveTrainingVM();

            //Act
            var result = await controller.Create(model);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveTrainingVM>(viewResult.Model);
        }

        [Fact]
        public async Task Create_RedirectToAction()
        {
            //Arrange
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "John Doe"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var model = new SaveTrainingVM();
            var model1 = new Trainning();
            mockMapper.Setup(repo => repo.Map<Trainning>(model)).Returns(model1);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.Complete()).ReturnsAsync(1);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, mockNotyfService.Object, null);
            mockHttpContext.Setup(repo => repo.User).Returns(claimsPrincipal);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = await controller.Create(model);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Account", redirectToAction.ControllerName);
            Assert.Equal("GetTrainingsForCompany", redirectToAction.ActionName);
        }

        [Fact]
        public async Task Edit_NotFound()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);

            //Act
            var result = await controller.Edit(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ViewResult()
        {
            //Arrange
            mockMapper.Setup(repo => repo.Map<SaveTrainingVM>(new Trainning())).Returns(new SaveTrainingVM());
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR1);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.Edit(1);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_InvalidModel()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);
            controller.ModelState.AddModelError("Title", "Title is required");

            //Act
            var result = await controller.Edit(1, new SaveTrainingVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveTrainingVM>(viewResult.Model);
        }

        [Fact]
        public async Task Edit_ValidModel_NotFound()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);

            //Act
            var result = await controller.Edit(1, new SaveTrainingVM() { Id = 2 });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ValidModel_RedirectToAction()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.Complete()).ReturnsAsync(1);
            mockMapper.Setup(repo => repo.Map<Trainning>(new SaveTrainingVM())).Returns(new Trainning());
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, mockNotyfService.Object, null);

            //Act
            var result = await controller.Edit(1, new SaveTrainingVM() { Id = 1 });

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Account", redirectToAction.ControllerName);
            Assert.Equal("GetTrainingsForCompany", redirectToAction.ActionName);
        }

        [Fact]
        public async Task Edit_ValidModel_ViewResult()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.Complete()).ReturnsAsync(0);
            var model = new SaveTrainingVM() { Id = 1 };
            mockMapper.Setup(repo => repo.Map<Trainning>(model)).Returns(new Trainning());
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, mockNotyfService.Object, null);

            //Act
            var result = await controller.Edit(1, new SaveTrainingVM() { Id = 1 });

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<SaveTrainingVM>(viewResult.Model);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.Complete()).ReturnsAsync(1);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);

            //Act
            var result = await controller.Delete(0);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_BadRequest()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWrok.Setup(repo => repo.Complete()).ReturnsAsync(0);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);

            //Act
            var result = await controller.Delete(0);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ApplyForTraining_NotFound()
        {
            //Arrange
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "John Doe"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            mockHttpContext.Setup(repo => repo.User).Returns(claimsPrincipal);
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = await controller.ApplyForTraining(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ApplyForTraining_ResultSucceeded()
        {
            //Arrange
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "John Doe"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            mockHttpContext.Setup(repo => repo.User).Returns(claimsPrincipal);
            var fakeTR2 = new FakeTrainingRepository(new Trainning(), 1, false);
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR2);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockNotyfService.Setup(repo => repo.Success("Applied Successfully.", null)).Verifiable();
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, mockNotyfService.Object, null);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = await controller.ApplyForTraining(1);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToAction.ActionName);
            mockNotyfService.Verify();
        }

        [Fact]
        public async Task ApplyForTraining_ResultFailed()
        {
            //Arrange
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "John Doe"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            mockHttpContext.Setup(repo => repo.User).Returns(claimsPrincipal);
            var fakeTR2 = new FakeTrainingRepository(new Trainning(), 0, false);
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR2);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockNotyfService.Setup(repo => repo.Error("Applying did not complete.", null)).Verifiable();
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, mockNotyfService.Object, null);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = await controller.ApplyForTraining(1);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToAction.ActionName);
            mockNotyfService.Verify();
        }

        [Fact]
        public async Task GetApplicationsForTraining_NotFound()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new TrainingsController(mockUnitOfWrok.Object, null, null, null);

            //Act
            var result = await controller.GetApplicationsForTraining(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetApplicationsForTraining_ViewResult()
        {
            //Arrange
            mockUnitOfWrok.Setup(repo => repo.TrainningRepository()).Returns(fakeTR1);
            mockUnitOfWrok.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockMapper.Setup(repo => repo.Map<TrainingVM>(new Trainning())).Returns(new TrainingVM());
            var controller = new TrainingsController(mockUnitOfWrok.Object, mockMapper.Object, null, null);

            //Act
            var result = await controller.GetApplicationsForTraining(1);

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
