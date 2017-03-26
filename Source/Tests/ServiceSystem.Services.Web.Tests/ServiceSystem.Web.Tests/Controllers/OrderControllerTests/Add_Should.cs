using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Services.Web;
using ServiceSystem.Web.Controllers;
using ServiceSystem.Web.ViewModels.Order;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Add_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderController).Assembly });
        }

        [TestMethod]
        public void RenderDefaultViewWithListOfCategories()
        {
            // Arrange
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedCacheService = new Mock<ICacheService>();

            var categories = new List<CategoryModel>()
            {
                new CategoryModel()
                {
                    Id = 1,
                     Name = "category 1"
                },
                new CategoryModel()
                {
                    Id = 2,
                    Name = "category 2"
                }
            };

            mockedCategoriesService.Setup(cs => cs.GetAll()).Returns(categories);
            mockedCacheService.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<Func<IEnumerable<CategoryModel>>>(), It.IsAny<int>())).Returns(categories);
            var testedOrderController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object);
            testedOrderController.Cache = mockedCacheService.Object;

            // Act & Assert
            testedOrderController
                .WithCallTo(c => c.Add())
                .ShouldRenderDefaultView()
                .WithModel<OrderCreateModel>(m =>
                {
                    Assert.AreEqual(m.Categories.First().Value, "1");
                    Assert.AreEqual(m.Categories.First().Text, "category 1");
                });
        }

        [TestMethod]
        public void ReturnDefaultView_WhenModelIsNotValidOnPOSTRequest()
        {
            // Arrange
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedCacheService = new Mock<ICacheService>();
            mockedCacheService.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<Func<IEnumerable<CategoryModel>>>(), It.IsAny<int>())).Returns(new List<CategoryModel>());

            var model = new OrderCreateModel();
            var testedController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object);
            testedController.Cache = mockedCacheService.Object;

            // Act & Assert
            testedController
                .WithModelErrors()
                .WithCallTo(c => c.Add(model))
                .ShouldRenderDefaultView()
                .WithModel<OrderCreateModel>(m => Assert.AreSame(model, m));
        }

        [TestMethod]
        public void ReturnError_WhenOrderCreationServiceThrows()
        {
            // Arrange
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedHttpContext = new Mock<HttpContextBase>();
            var mockedResponse = new Mock<HttpResponseBase>();

            mockedCacheService.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<Func<IEnumerable<CategoryModel>>>(), It.IsAny<int>())).Returns(new List<CategoryModel>());
            mockedOrderService.Setup(c => c.Create(It.IsAny<OrderModel>())).Throws<ArgumentException>();

            var model = new OrderCreateModel();

            var testedController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object);

            testedController.Cache = mockedCacheService.Object;

            mockedHttpContext.SetupGet(x => x.Response).Returns(mockedResponse.Object);
            testedController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockedHttpContext.Object
            };

            // Act & Assert
            testedController
                .WithCallTo(c => c.Add(model))
                .ShouldRenderDefaultView()
                .WithModel<OrderCreateModel>(m => Assert.AreSame(model, m));
            Assert.IsNotNull(testedController.TempData["Error"]);
        }

        [TestMethod]
        public void RedirectToActionSlip_WhenOrderCreationIsSuccessfull()
        {
            // Arrange
            int id = 2;
            var mockedOrderService = new Mock<IOrderService>();
            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            mockedContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("name");

            mockedOrderService.Setup(os => os.Create(It.IsAny<OrderModel>())).Returns(new OrderModel() { Id = id });

            var model = new OrderCreateModel();

            var testedController = new OrderController(mockedCategoriesService.Object, mockedOrderService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockedContext.Object
                }
            };

            // Act & Assert
            testedController
                .WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(oc => oc.Slip(id));
        }
    }
}
