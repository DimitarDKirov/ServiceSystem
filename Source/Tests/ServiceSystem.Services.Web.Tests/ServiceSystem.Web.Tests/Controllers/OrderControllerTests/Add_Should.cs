using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Services.Web;
using ServiceSystem.Web.Controllers;
using ServiceSystem.Web.ViewModels.Order;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.OrderControllerTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void RenderDefaultViewWithListOfCategories()
        {
            //Arrange
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

            //Act & Assert
            testedOrderController
                .WithCallTo(c => c.Add())
                .ShouldRenderDefaultView()
                .WithModel<OrderCreateModel>(m =>
                {
                    Assert.AreEqual(m.Categories.First().Value, "1");
                    Assert.AreEqual(m.Categories.First().Text, "category 1");
                });
        }
    }
}
