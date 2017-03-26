using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenBothParametersAreNotNull()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var orderServiceMock = new Mock<IOrderService>();

            // Act
            var testedController = new OrderController(categoryServiceMock.Object, orderServiceMock.Object);

            // Assert
            Assert.IsNotNull(testedController);
        }

        [TestMethod]
        public void Throw_WhenCategoryServiceParameterIsNull()
        {
            // Arrange
            var orderServiceMock = new Mock<IOrderService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderController(null, orderServiceMock.Object));
        }

        [TestMethod]
        public void Throw_WhenOrdersServiceParameterIsNull()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderController(categoryServiceMock.Object, null));
        }

    }
}
