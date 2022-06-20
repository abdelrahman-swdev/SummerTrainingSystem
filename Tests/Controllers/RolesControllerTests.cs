using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Controllers;
using SummerTrainingSystem.Models;
using Tests.TestClasses;

namespace Tests.Controllers
{
    public class RolesControllerTests
    {
        [Fact]
        public async Task CreateRole_InvalidModel()
        {
            //Arrange
            var controller = new RolesController(null);
            controller.ModelState.AddModelError("Name", "Required");

            //Act
            var result = await controller.CreateRole(new CreateRoleVM());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CreateRoleVM>(viewResult.Model);
        }

        [Fact]
        public async Task CreateRole_ValidModel()
        {
            //Arrange
            var fakeRoleManager = new FakeRoleManager(true);
            var controller = new RolesController(fakeRoleManager);

            //Act
            var result = await controller.CreateRole(new CreateRoleVM());

            //Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ListRoles", redirectToAction.ActionName);
        }

        [Fact]
        public async Task DeleteRole_Ok()
        {
            //Arrange
            var fakeRoleManager = new FakeRoleManager(true);
            var controller = new RolesController(fakeRoleManager);

            //Act
            var result = await controller.DeleteRole("0");

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteRole_BadRequest()
        {
            //Arrange
            var fakeRoleManager = new FakeRoleManager();
            var controller = new RolesController(fakeRoleManager);

            //Act
            var result = await controller.DeleteRole("0");

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
