using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.DateProvider;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.OrderServiceTests
{
    [TestClass]
    public class Assign_Should
    {
        [TestInitialize]
        public void Initialize()
        {
            DateTimeProvider.Current = new TestingDateTimeProvider();
        }

        [TestMethod]
        public void UpdateOrderAndSaveChanges_WhenOrderIsAssignable()
        {
            // Arrange
            var orderId = 2;
            var userId = "user";
            var existingOrderStub = new Order() { Status = Status.Pending, Id = orderId };
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(existingOrderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object);

            // Act
            testedService.Assign(orderId, userId);

            // Assert
            mockedOrderRepo.Verify(
                r => r.Update(
                    It.Is<Order>(o =>
              o.UserId == userId
              && o.Id == orderId
              && o.Status == Status.InProcess
              && o.RepairStartDate == DateTimeProvider.Current.UtcNow)), Times.Once);
            mockedSaveChagesRepo.Verify(sc => sc.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeException_WhenOrderIWthGivenIdIsNotFound()
        {
            // Arrange
            var orderId = 2;
            var userId = "user";
            var existingOrderStub = new Order() { Status = Status.Pending, Id = orderId };
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testedService.Assign(orderId, userId));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenFoundOrderIsWithImproperStatus()
        {
            // Arrange
            var orderId = 2;
            var userId = "user";
            var existingOrderStub = new Order() { Status = Status.Parts, Id = orderId };
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(existingOrderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => testedService.Assign(orderId, userId), "You can not be assigned to this order");
        }

        [TestCleanup]
        public void Clean()
        {
            DateTimeProvider.ResetToDefault();
        }
    }
}
