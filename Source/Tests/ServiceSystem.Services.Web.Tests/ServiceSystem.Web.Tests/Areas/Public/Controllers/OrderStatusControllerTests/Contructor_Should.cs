using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Areas.Public.Controllers;
using System;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Public.Controllers.OrderStatusControllerTests
{
    [TestClass]
    public class Contructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            // Act
            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            // Assert
            Assert.IsNotNull(testedController);
        }

        [TestMethod]
        public void Throw_WhenCoderServiceIsNull()
        {
            // Arrange
            var mockedOrderService = new Mock<IOrderService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderStatusController(null, mockedOrderService.Object));
        }

        [TestMethod]
        public void Throw_WhenOrderServiceIsNull()
        {
            // Arrange
            var mockedCoderService = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderStatusController(mockedCoderService.Object, null));
        }
    }
}
