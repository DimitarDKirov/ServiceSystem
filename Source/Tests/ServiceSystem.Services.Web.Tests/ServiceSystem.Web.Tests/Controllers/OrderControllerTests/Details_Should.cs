using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.Controllers;
using ServiceSystem.Web.ViewModels.Order;
using TestStack.FluentMVCTesting;
using System.Security.Principal;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Details_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderController).Assembly });
        }

        [TestMethod]
        public void DefaultViewWithEditableTrue_WhenOrderIsFoundWithProperStatus()
        {
            // Arrange
            int searchedId = 2;
            var ordersModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            testedOrderController
                .WithCallTo(c => c.Details(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(m =>
                {
                    Assert.AreEqual(searchedId, m.Id);
                    Assert.IsTrue(m.IsEditable);
                });
        }

        [TestMethod]
        public void DefaultViewWithEditableFalse_WhenOrderIsFoundWithStatusePending()
        {
            // Arrange
            int searchedId = 2;
            var ordersModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.Delivered
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            testedOrderController
                .WithCallTo(c => c.Details(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(m =>
                {
                    Assert.AreEqual(searchedId, m.Id);
                    Assert.IsFalse(m.IsEditable);
                });
        }

        [TestMethod]
        public void DefaultViewWithEditableFalse_WhenOrderIsFoundWithUserNotAsiggnee()
        {
            // Arrange
            int searchedId = 2;
            var ordersModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess,
                UserId = "userid"
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            testedOrderController
                .WithCallTo(c => c.Details(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(m =>
                {
                    Assert.AreEqual(searchedId, m.Id);
                    Assert.IsFalse(m.IsEditable);
                });
        }

        [TestMethod]
        public void ReturnError_WhenOrderIsNotFound()
        {
            // Arrange
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedHttpContext = new Mock<HttpContextBase>();
            var mockedResponse = new Mock<HttpResponseBase>();

            mockedOrderService.Setup(s => s.GetById(It.IsAny<int>())).Returns((OrderModel)null);

            var testedController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object);

            mockedHttpContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);
            testedController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockedHttpContext.Object
            };

            // Act & Assert
            testedController
                .WithCallTo(c => c.Details(2))
                .ShouldRenderView("Error");
        }
    }
}
