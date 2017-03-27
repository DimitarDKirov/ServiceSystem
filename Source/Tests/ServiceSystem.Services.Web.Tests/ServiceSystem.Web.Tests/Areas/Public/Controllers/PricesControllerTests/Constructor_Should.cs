using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Areas.Public.Controllers;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Public.Controllers.PricesControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenCategoriesServiceIsNotNull()
        {
            // Arrange
            var mockedCategoriesService = new Mock<ICategoryService>();

            // Act
            var testedController = new PricesController(mockedCategoriesService.Object);

            // Assert
            Assert.IsNotNull(testedController);
        }

        [TestMethod]
        public void Throw_WhenCategoriesServiceIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PricesController(null));
        }
    }
}
