using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.Areas.Administration.Controllers;
using ServiceSystem.Web.Areas.Administration.Models.Categories;
using ServiceSystem.Web.Areas.Public.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Administration.Controllers.CategoriesControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderStatusController).Assembly });
        }

        [TestMethod]
        public void ReturnDefaultView_WithCategoriesAsModel()
        {
            //Arrange
            var categoriesStub = new List<CategoryModel>
            {
                new CategoryModel()
                {
                    Id=1,
                    MaxPrice=50,
                    Name="test role"
                },
                 new CategoryModel()
                {
                    Id=2,
                    MaxPrice=40,
                    Name="test role2"
                }
            };

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(cs => cs.GetAll()).Returns(categoriesStub.AsEnumerable());

            var testedController = new CategoriesController(mockedCategoryService.Object);

            //Act & Assert
            testedController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<CategoriesViewModel>>(m =>
                {
                    Assert.AreEqual(categoriesStub[0].Id, m.First().Id);
                    Assert.AreEqual(categoriesStub[0].Name, m.First().Name);
                    Assert.AreEqual(categoriesStub[0].MaxPrice, m.First().MaxPrice);
                    Assert.AreEqual(categoriesStub[1].Id, m.Last().Id);
                    Assert.AreEqual(categoriesStub[1].Name, m.Last().Name);
                    Assert.AreEqual(categoriesStub[1].MaxPrice, m.Last().MaxPrice);
                });
        }
    }
}
