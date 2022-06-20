using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class CommentControllerTests
    {
        [Fact]
        public async Task AddComment_BadRequest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var controller = new CommentController(mockUnitOfWork.Object, null);
            controller.ModelState.AddModelError("Comment", "Required");

            //Act
            var result = await controller.AddComment(new CommentJsonVM());

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddComment_Ok()
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
            var model = new CommentJsonVM();
            var fakeGR = new FakeIGenericRepository<Student>(new Student());
            var fakeGR1 = new FakeIGenericRepository<Comment>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(repo => repo.Map<Comment>(model)).Returns(new Comment());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Student>()).Returns(fakeGR);
            mockUnitOfWork.Setup(repo => repo.GenericRepository<Comment>()).Returns(fakeGR1);
            mockUnitOfWork.Setup(repo => repo.Complete()).ReturnsAsync(1);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(repo => repo.User).Returns(claimsPrincipal);
            var controller = new CommentController(mockUnitOfWork.Object, mockMapper.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = await controller.AddComment(model);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
