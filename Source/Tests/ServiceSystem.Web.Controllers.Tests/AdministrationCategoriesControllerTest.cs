namespace MvcTemplate.Web.Controllers.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using ServiceSystem.Data.Models;
    using ServiceSystem.Services.Data;
    using ServiceSystem.Web.Areas.Administration.Controllers;
    using ServiceSystem.Web.Areas.Administration.Models.Categories;
    using ServiceSystem.Web.Infrastructure.Mapping;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class AdministrationCategoriesControllerTest
    {
        private IList<Category> categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "name1",
                MinPrice = 20,
                MaxPrice = 30
            },
            new Category
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
            autoMapperConfig.Execute(typeof(CategoriesController).Assembly);

            var categoriesServiceMock = new Mock<ICategoriesService>();
            categoriesServiceMock.Setup(x => x.GetAll())
                .Returns(this.categories.AsQueryable());
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
            autoMapperConfig.Execute(typeof(CategoriesController).Assembly);

            var categoriesServiceMock = new Mock<ICategoriesService>();
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
