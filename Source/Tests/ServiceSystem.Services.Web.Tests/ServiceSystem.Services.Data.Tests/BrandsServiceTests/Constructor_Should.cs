using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.BrandsServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenBrandsRepo_IsNotNull()
        {
            // Arrange
            var brandsRepoMock = new Mock<IEfDbRepository<Brand>>();

            // Act
            var testedService = new BrandsService(brandsRepoMock.Object);

            // Assert
            Assert.IsNotNull(testedService);
        }

        [TestMethod]
        public void Throw_WhenBrandsRepo_IsNotNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new BrandsService(null));
        }
    }
}
