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

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Slip_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderController).Assembly });
        }

        [TestMethod]
        public void DefaultView_WhenOrderIsFound()
        {
            // Arrange
            int searchedId = 2;
            var ordersModel = new OrderModel() { Id = 2 };
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object);

            testedOrderController
                .WithCallTo(c => c.Slip(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(m => Assert.AreEqual(searchedId, m.Id));
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
                .WithCallTo(c => c.Slip(2))
                .ShouldRenderView("Error");
        }
    }
}
