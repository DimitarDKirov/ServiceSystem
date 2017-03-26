using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.CategoryServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void AddDbModeWithCorrectPropertiesToTheRepo()
        {
            // Arrange
            var categoryName = "category";
            var minPrice = 20;
            var maxPrice = 60;
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.Create(categoryName, minPrice, maxPrice);

            // Assert
            categoriesRepoMock.Verify(cr => cr.Add(It.Is<Category>(c => c.Name == categoryName)), Times.Once);
            categoriesRepoMock.Verify(cr => cr.Add(It.Is<Category>(c => c.MinPrice == minPrice)), Times.Once);
            categoriesRepoMock.Verify(cr => cr.Add(It.Is<Category>(c => c.MaxPrice == maxPrice)), Times.Once);

        }

        [TestMethod]
        public void CallSaveChanges()
        {
            // Arrange
            var categoryName = "category";
            var minPrice = 20;
            var maxPrice = 60;
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.Create(categoryName, minPrice, maxPrice);

            // Assert
            saveChangesRepoMock.Verify(sc => sc.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Throw_IfNameIsNull()
        {
            // Arrange
            string categoryName = null;
            var minPrice = 20;
            var maxPrice = 60;
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => testedService.Create(categoryName, minPrice, maxPrice));
        }

        [TestMethod]
        public void Throw_IfNameIsEmpty()
        {
            // Arrange
            string categoryName = string.Empty;
            var minPrice = 20;
            var maxPrice = 60;
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => testedService.Create(categoryName, minPrice, maxPrice));
        }


    }
}
