using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrdersControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedService = new Mock<IOrderService>();

            // Act
            var testedController = new OrdersController(mockedService.Object);

            // Assert
            testedController.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
