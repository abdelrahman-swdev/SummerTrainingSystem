using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SummerTrainingSystem.Controllers;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.TestClasses;
using Xunit;

namespace Tests.Controllers
{
    public class DepartmentsControllerTests
    {
        [Fact]
        public async Task CreateDepartment_InvalidModel()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Department>(new Department());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new DepartmentsController(null, null, mockUnitOfWork.Object);
            controller.ModelState.AddModelError("Name", "Required");

            //Act
            var result = await controller.CreateDepartment(new CreateDepartmentVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CreateDepartmentVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateDepartment_RedirectToAction()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Department>(new Department());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWork.Setup(repo => repo.Complete()).ReturnsAsync(1);
            var model = new CreateDepartmentVM();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(repo => repo.Map<Department>(model)).Returns(new Department());
            var mockNotyfService = new Mock<INotyfService>();
            mockNotyfService.Setup(repo => repo.Success("Department created successfully", null)).Verifiable();
            var controller = new DepartmentsController(mockMapper.Object, mockNotyfService.Object, mockUnitOfWork.Object);

            //Act
            var result = await controller.CreateDepartment(model);

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetDepartments", redirectToAction.ActionName);
            mockUnitOfWork.Verify();
        }

        [Fact]
        public async Task DeleteDepartment_BadRequest()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Department>(new Department());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWork.Setup(repo => repo.Complete()).ReturnsAsync(0);
            var controller = new DepartmentsController(null, null, mockUnitOfWork.Object);

            //Act
            var result = await controller.DeleteDepartment(0);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteDepartment_Ok()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Department>(new Department());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            mockUnitOfWork.Setup(repo => repo.Complete()).ReturnsAsync(1);
            var controller = new DepartmentsController(null, null, mockUnitOfWork.Object);

            //Act
            var result = await controller.DeleteDepartment(0);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetGetDepartments()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<Department>(new Department());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Department>()).Returns(fakeGR);
            var controller = new DepartmentsController(null, null, mockUnitOfWork.Object);

            //Act
            var result = await controller.GetDepartments();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<Department>>(viewResult.Model);
        }
    }
}
