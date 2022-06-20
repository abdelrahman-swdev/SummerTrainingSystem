using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SummerTrainingSystem.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class ErrorsControllerTests
    {
        Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        Mock<ILogger<ErrorsController>> mockILogger = new Mock<ILogger<ErrorsController>>();
        Mock<IFeatureCollection> mockIFeatureCollection = new Mock<IFeatureCollection>();
        Mock<IStatusCodeReExecuteFeature> mockIStatusCodeReExecuteFeature = new Mock<IStatusCodeReExecuteFeature>();

        [Fact]
        public void HandleErrors_ViewResult()
        {
            //Arrange
            mockIStatusCodeReExecuteFeature.Setup(repo => repo.OriginalPath).Returns(string.Empty);
            mockIStatusCodeReExecuteFeature.Setup(repo => repo.OriginalQueryString).Returns(string.Empty);
            mockIFeatureCollection.Setup(repo => repo.Get<IStatusCodeReExecuteFeature>()).Returns(mockIStatusCodeReExecuteFeature.Object);
            mockHttpContext.Setup(repo => repo.Features).Returns(mockIFeatureCollection.Object);
            var controller = new ErrorsController(mockILogger.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = controller.HandleErrors(404);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public void HandleErrors_BadRequest()
        {
            //Arrange
            mockIFeatureCollection.Setup(repo => repo.Get<IStatusCodeReExecuteFeature>()).Returns(mockIStatusCodeReExecuteFeature.Object);
            mockHttpContext.Setup(repo => repo.Features).Returns(mockIFeatureCollection.Object);
            var controller = new ErrorsController(mockILogger.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = controller.HandleErrors(405);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void HandleExceptions()
        {
            //Arrange
            var mockIExceptionHandlerPathFeature = new Mock<IExceptionHandlerPathFeature>();
            mockIExceptionHandlerPathFeature.Setup(repo => repo.Path).Returns(string.Empty);
            mockIExceptionHandlerPathFeature.Setup(repo => repo.Error).Returns(new System.Exception());
            mockIFeatureCollection.Setup(repo => repo.Get<IExceptionHandlerPathFeature>()).Returns(mockIExceptionHandlerPathFeature.Object);
            mockHttpContext.Setup(repo => repo.Features).Returns(mockIFeatureCollection.Object);
            var controller = new ErrorsController(mockILogger.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var result = controller.HandleExceptions();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}
