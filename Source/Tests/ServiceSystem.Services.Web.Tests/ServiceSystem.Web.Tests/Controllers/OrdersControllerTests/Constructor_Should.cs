using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Controllers;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrdersControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenOrdersServiceIsPassed()
        {
            // Arrange
            var mockedService = new Mock<IOrderService>();

            // Act
            var testedController = new OrdersController(mockedService.Object);

            // Assert
            Assert.IsNotNull(testedController);
        }

        [TestMethod]
        public void Throw_WhenOrderServiceIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrdersController(null));
        }
    }
}
