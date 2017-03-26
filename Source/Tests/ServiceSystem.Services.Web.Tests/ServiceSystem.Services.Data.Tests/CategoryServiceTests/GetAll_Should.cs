using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.CategoryServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [ClassInitialize]
        public static void ConfigAutomapper(TestContext context)
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(CategoryService).Assembly });
        }

        [TestMethod]
        public void Call_MethodAllOfEFRepository()
        {
            // Arrange
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.GetAll();

            // Assert
            categoriesRepoMock.Verify(cr => cr.All(), Times.Once);
        }

        [TestMethod]
        public void ReturnResultsOfTypeCategoryModel()
        {
            // Arrange
            var categoriesStub = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "category 1"
                },
                new Category()
                {
                    Id = 2,
                    Name = "category 2"
                }
            };

            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();
            categoriesRepoMock.Setup(cm => cm.All()).Returns(categoriesStub.AsQueryable());

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            var result = testedService.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CategoryModel>));
            Assert.AreEqual(categoriesStub.Count, result.Count());
            Assert.AreEqual(categoriesStub[0].Name, result.First().Name);
            Assert.AreEqual(categoriesStub[1].Name, result.Last().Name);
        }
    }
}
