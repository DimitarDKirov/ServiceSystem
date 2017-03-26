using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Assign_Should
    {
        [TestMethod]
        public void AssignOrderToUser_AndRedirectToEditPage()
        {
            // Arrange
            int orderId = 2;
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.Assign(It.IsAny<int>(), It.IsAny<string>())).Verifiable();

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act
            testedOrderController.Assign(orderId);

            // Assert
            mockedOrderService.Verify(s => s.Assign(It.Is<int>(id => id == orderId), It.IsAny<string>()), Times.Once);

            StringAssert.Contains(testedOrderController.TempData["Success"].ToString(), "assigned to order " + orderId);
            testedOrderController
                 .WithCallTo(c => c.Assign(orderId))
                 .ShouldRedirectTo(c => c.Details(orderId));
        }

        [TestMethod]
        public void ReturnErrorView_WhenOrderIsNotFound()
        {
            // Arrange
            int orderId = 2;

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedResponse = new Mock<HttpResponseBase>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockedContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);

            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.Assign(It.IsAny<int>(), It.IsAny<string>())).Throws<ArgumentOutOfRangeException>();

            var testedController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act & Assert
            testedController
                .WithCallTo(c => c.Assign(orderId))
                .ShouldRenderView("Error");
        }

        [TestMethod]
        public void RedirectToDetailsWuthError_WhenUserCanNotBeAssigned()
        {
            // Arrange
            int orderId = 2;
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.Assign(It.IsAny<int>(), It.IsAny<string>())).Throws<ArgumentException>();

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act
            testedOrderController.Assign(orderId);

            // Assert
            testedOrderController
                 .WithCallTo(c => c.Assign(orderId))
                 .ShouldRedirectTo(c => c.Details(orderId));
        }
    }
}
