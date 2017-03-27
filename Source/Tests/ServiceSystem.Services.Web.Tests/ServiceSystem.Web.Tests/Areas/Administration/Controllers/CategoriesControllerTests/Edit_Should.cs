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
    public class Edit_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(CategoriesController).Assembly });
        }

        [TestMethod]
        public void ReturnDefaultViewWithModel_WhenSelectedCategoryExisists()
        {
            //Arrange
            var searchedId = 2;
            var categoryStub = new CategoryModel()
            {
                Id = 2,
                Name = "category",
                MaxPrice = 50
            };

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(cs => cs.Find(It.IsAny<int>())).Returns(categoryStub);

            var testedController = new CategoriesController(mockedCategoryService.Object);

            //Act
            var result = testedController.Edit(searchedId);

            //Assert
            testedController
                .WithCallTo(x => x.Edit(searchedId))
                .ShouldRenderDefaultView()
                .WithModel<CategoryEditModel>(
                        model =>
                        {
                            Assert.AreEqual(model.Id, categoryStub.Id);
                            Assert.AreEqual(model.Name, categoryStub.Name);
                            Assert.AreEqual(model.MaxPrice, categoryStub.MaxPrice);
                        })
                        .AndNoModelErrors();
        }
    }
}
