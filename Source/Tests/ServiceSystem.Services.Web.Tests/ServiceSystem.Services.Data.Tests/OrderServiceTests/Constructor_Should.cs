using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.OrderServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenAllParametersArePassed()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act
            var testedService = new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object);

            // Assert
            Assert.IsNotNull(testedService);
        }

        [TestMethod]
        public void Throw_WhenOrdersRepositoryIsNull()
        {
            // Arrange
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                null,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object));
        }

        [TestMethod]
        public void Throw_WhenSaveChangesRepositoryIsNull()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                mockedOrderRepo.Object,
                null,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object));
        }

        [TestMethod]
        public void Throw_WhenMappingServiceIsNull()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                null,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object));
        }

        [TestMethod]
        public void Throw_WhenUnitServiceIsNull()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                null,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object));
        }

        [TestMethod]
        public void Throw_WhenCustomerServiceIsNull()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                null,
                mockedPublicCodeProvider.Object));
        }

        [TestMethod]
        public void Throw_WhenPublicCodeServiceIsNull()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                null));
        }
    }
}
