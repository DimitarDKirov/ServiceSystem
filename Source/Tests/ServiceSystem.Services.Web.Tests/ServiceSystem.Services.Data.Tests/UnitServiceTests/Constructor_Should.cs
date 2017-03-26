using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.UnitServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenBothParametersArePassed()
        {
            // Arrange
            var mockedBrandsService = new Mock<IBrandsService>();
            var mockedMappingService = new Mock<IMappingService>();

            // Act
            var testedService = new UnitService(mockedBrandsService.Object, mockedMappingService.Object);

            // Assert
            Assert.IsNotNull(testedService);
        }

        [TestMethod]
        public void Throw_WhenBrandServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UnitService(null, mockedMappingService.Object));
        }

        [TestMethod]
        public void Throw_WhenmappringServiceIsNull()
        {
            // Arrange
            var mockedBrandsService = new Mock<IBrandsService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UnitService(mockedBrandsService.Object, null));
        }

    }
}
