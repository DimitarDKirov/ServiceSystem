using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.CategoryServiceTests
{
    [TestClass]
    public class Find_Should
    {
        [TestMethod]
        public void SearchTheRepo_WithPassedId()
        {
            // Arrange
            int searchedId = 5;
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();
            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.Find(searchedId);

            // Assert
            categoriesRepoMock.Verify(cr => cr.GetById(It.Is<int>(s => s == searchedId)), Times.Once);
        }

        [TestMethod]
        public void SearchTheRepo_AndMapToCategoryModel()
        {
            // Arrange
            int searchedId = 5;
            var categoryStub = new Category()
            {
                Id = searchedId,
                Name = "category name"
            };

            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            categoriesRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(categoryStub);

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.Find(searchedId);

            // Assert
            mappingServiceMock.Verify(ms => ms.Map<CategoryModel>(It.Is<Category>(c => c == categoryStub)), Times.Once);
        }

        [TestMethod]
        public void ReturnCategoryModel()
        {
            // Arrange
            int searchedId = 5;
            var categoryStub = new Category()
            {
                Id = searchedId,
                Name = "category name"
            };
            var categoryModelStub = new CategoryModel()
            {
                Id = searchedId,
                Name = "category name"
            };

            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            mappingServiceMock.Setup(ms => ms.Map<CategoryModel>(It.IsAny<CategoryModel>())).Returns(categoryModelStub);

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            var result = testedService.Find(searchedId);

            // Assert
            Assert.AreSame(categoryModelStub, result);
        }
    }
}
