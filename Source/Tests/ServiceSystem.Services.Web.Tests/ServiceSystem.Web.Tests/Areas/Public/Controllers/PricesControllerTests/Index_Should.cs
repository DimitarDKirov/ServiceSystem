using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Services.Web;
using ServiceSystem.Web.Areas.Public.Controllers;
using ServiceSystem.Web.Areas.Public.Models.Prices;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Public.Controllers.PricesControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(PricesController).Assembly });
        }

        [TestMethod]
        public void ReturnDefaultViewWithPricesViewModel()
        {
            // Arrange
            var categories = new List<CategoryModel>()
            {
                new CategoryModel()
                {
                    MaxPrice = 20,
                    MinPrice = 10,
                    Name = "category 1"
                },
                new CategoryModel()
                {
                    MaxPrice = 30,
                    MinPrice = 20,
                    Name = "category 2"
                }
            };

            var mockedCategoriesService = new Mock<ICategoryService>();
            var mockedCacheService = new Mock<ICacheService>();

            mockedCategoriesService.Setup(cs => cs.GetAll()).Returns(categories);
            mockedCacheService.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<Func<IEnumerable<CategoryModel>>>(), It.IsAny<int>())).Returns(categories);

            var testedController = new PricesController(mockedCategoriesService.Object);
            testedController.Cache = mockedCacheService.Object;

            // Act & Assert
            testedController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<PricesViewModel>>(m =>
               {
                   Assert.AreEqual(categories[0].MaxPrice, m.First().MaxPrice);
                   Assert.AreEqual(categories[0].MinPrice, m.First().MinPrice);
                   Assert.AreEqual(categories[0].Name, m.First().Name);
                   Assert.AreEqual(categories[1].MaxPrice, m.Last().MaxPrice);
                   Assert.AreEqual(categories[1].MinPrice, m.Last().MinPrice);
                   Assert.AreEqual(categories[1].Name, m.Last().Name);
               });
        }
    }
}
