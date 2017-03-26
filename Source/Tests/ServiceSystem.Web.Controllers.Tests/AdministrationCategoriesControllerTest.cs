using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.Areas.Administration.Controllers;
using ServiceSystem.Web.Areas.Administration.Models.Categories;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.Tests
{
    [TestFixture]
    public class AdministrationCategoriesControllerTest
    {
        private IList<CategoryModel> categories = new List<CategoryModel>
        {
            new CategoryModel
            {
                Id = 1,
                Name = "name1",
                MinPrice = 20,
                MaxPrice = 30
            },
            new CategoryModel
            {
                Id = 2,
                Name = "name2",
                MinPrice = 20,
                MaxPrice = 30
            }
        };

        [Test]
        public void IndexShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(new[] { typeof(CategoriesController).Assembly });

            var categoriesServiceMock = new Mock<ICategoryService>();
            categoriesServiceMock.Setup(x => x.GetAll())
                .Returns(this.categories);
            var controller = new CategoriesController(categoriesServiceMock.Object);
            controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<IEnumerable<CategoriesViewModel>>(
                        model => Assert
                        .AreEqual(model.First().Id, this.categories[0].Id))
                        .AndNoModelErrors();
        }

        [Test]
        public void EditShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(new[] { typeof(CategoriesController).Assembly });

            var categoriesServiceMock = new Mock<ICategoryService>();
            categoriesServiceMock.Setup(x => x.Find(It.IsAny<int>()))
                .Returns(this.categories[0]);
            var controller = new CategoriesController(categoriesServiceMock.Object);
            controller.WithCallTo(x => x.Edit(0))
                .ShouldRenderView("Edit")
                .WithModel<CategoryEditModel>(
                        model => Assert.AreEqual(model.Id, this.categories[0].Id))
                        .AndNoModelErrors();
        }
    }
}
