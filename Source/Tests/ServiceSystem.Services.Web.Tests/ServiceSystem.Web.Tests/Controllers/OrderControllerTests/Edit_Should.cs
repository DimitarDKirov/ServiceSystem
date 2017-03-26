using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.Controllers;
using ServiceSystem.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Edit_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderController).Assembly });
        }

        [TestMethod]
        public void ReturnDefaultView_WhenOrderIsEditable()
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

            // Act & Assert
            testedOrderController
                .WithCallTo(c => c.Edit(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(m =>
                {
                    Assert.AreEqual(searchedId, m.Id);
                });
        }

        [TestMethod]
        public void UpdateDataOnPostRequest_WhenOrderIsEditableAndModelIsValid()
        {
            // Arrange
            var orderUpdateModel = new OrderUpdateModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess,
                WarrantyStatus = Data.Models.WarrantyStatus.Rejected,
                LabourPrice = 20,
                ProblemDescription = "problem",
                Solution = "solution"
            };

            var orderModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();

            mockedOrderService.Setup(os => os.GetById(It.IsAny<int>())).Returns(orderModel);

            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act
            var result = testedOrderController.Edit(orderUpdateModel);

            // Assert
            mockedOrderService.Verify(os => os.Update(It.Is<OrderModel>(m =>
              m.Id == orderUpdateModel.Id
              && m.Status == orderUpdateModel.Status
              && m.WarrantyStatus == orderUpdateModel.WarrantyStatus
              && m.LabourPrice == orderUpdateModel.LabourPrice
              && m.ProblemDescription == orderUpdateModel.ProblemDescription
              && m.Solution == orderUpdateModel.Solution
            )));

            testedOrderController
                .WithCallTo(c => c.Edit(orderUpdateModel))
                .ShouldRedirectTo(c => c.Details(orderUpdateModel.Id));
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
                .WithCallTo(c => c.Edit(2))
                .ShouldRenderView("Error");
        }

        [TestMethod]
        public void RedirectToDetails_WhenOrderIsNotEditableOfStatus()
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
            var mockedResponse = new Mock<HttpResponseBase>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockedContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);

            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act & Assert
            testedOrderController
                .WithCallTo(c => c.Edit(searchedId))
                .ShouldRedirectTo(a => a.Details(ordersModel.Id));
        }

        [TestMethod]
        public void RedirectToDetails_WhenOrderIsNotEditableByUser()
        {
            // Arrange
            int searchedId = 2;
            var ordersModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess,
                UserId = "user"
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedResponse = new Mock<HttpResponseBase>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockedContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == searchedId))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act & Assert
            testedOrderController
                .WithCallTo(c => c.Edit(searchedId))
                .ShouldRedirectTo(a => a.Details(ordersModel.Id));
        }

        [TestMethod]
        public void ReturnErrorOnPostRequest_WhenOrderIsNotFound()
        {
            // Arrange
            var orderUpdateModel = new OrderUpdateModel()
            {
                Id = 2,
            };

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
                .WithCallTo(c => c.Edit(orderUpdateModel))
                .ShouldRenderView("Error");
        }

        [TestMethod]
        public void RedirectToDetailsOnPOstRequest_WhenOrderIsNotEditableOfStatus()
        {
            // Arrange
            var orderUpdateModel = new OrderUpdateModel()
            {
                Id = 2,
            };
            var ordersModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.Delivered
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedResponse = new Mock<HttpResponseBase>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockedContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);

            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.GetById(It.Is<int>(s => s == orderUpdateModel.Id))).Returns(ordersModel);

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act
            testedOrderController.Edit(orderUpdateModel);
            testedOrderController.TempData["Error"].ToString().Contains("not allowed ");

            // Assert
            testedOrderController
                .WithCallTo(c => c.Edit(orderUpdateModel))
                .ShouldRedirectTo(a => a.Details(ordersModel.Id));
        }

        [TestMethod]
        public void ReturnDefaultViewOnPost_WhenModelIsInvalid()
        {
            // Arrange
            var orderUpdateModel = new OrderUpdateModel()
            {
                Id = 2,
            };

            var orderModel = new OrderModel()
            {
                Id = 2,
                Status = Data.Models.Status.InProcess
            };

            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();

            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();

            mockedOrderService.Setup(os => os.GetById(It.IsAny<int>())).Returns(orderModel);

            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act & Assert
            testedOrderController
                .WithModelErrors()
                .WithCallTo(oc => oc.Edit(orderUpdateModel))
                .ShouldRenderDefaultView()
                .WithModel<OrderUpdateModel>(orderUpdateModel);
        }
    }
}