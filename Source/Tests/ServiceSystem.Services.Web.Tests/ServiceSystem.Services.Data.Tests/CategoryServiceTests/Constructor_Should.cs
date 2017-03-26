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
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenAllPArametersAreNotNull()
        {
            // Arrange
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            // Act
            var testedService = new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, mappingServiceMock.Object);

            // Assert
            Assert.IsNotNull(testedService);
        }

        [TestMethod]
        public void Throw_WhenCategoriesRepoIsNull()
        {
            // Arrange
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();
            var mappingServiceMock = new Mock<IMappingService>();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoryService(null, saveChangesRepoMock.Object, mappingServiceMock.Object));
        }

        [TestMethod]
        public void Throw_WhenEfRepoSaveChangesIsNull()
        {
            // Arrange
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var mappingServiceMock = new Mock<IMappingService>();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoryService(categoriesRepoMock.Object, null, mappingServiceMock.Object));
        }

        [TestMethod]
        public void Throw_WhenMappingServiceIsNull()
        {
            // Arrange
            var categoriesRepoMock = new Mock<IEfDbRepository<Category>>();
            var saveChangesRepoMock = new Mock<IEfDbRepositorySaveChanges>();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoryService(categoriesRepoMock.Object, saveChangesRepoMock.Object, null));
        }
    }
}
