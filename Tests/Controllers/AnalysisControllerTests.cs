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
    public class AnalysisControllerTests
    {
        [Fact]
        public async Task Index()
        {
            //Arrange
            var fakeGR = new FakeIGenericRepository<HrCompany>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.GenericRepository<HrCompany>()).Returns(fakeGR);
            var controller = new AnalysisController(null, mockUnitOfWork.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<SentimentAnalysisVM>>(viewResult.Model);
        }
    }
}
