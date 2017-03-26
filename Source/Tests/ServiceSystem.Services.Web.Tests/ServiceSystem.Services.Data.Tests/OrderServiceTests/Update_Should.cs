using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.OrderServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void UpdateExsitingDbModelWithValuesOfPassedModel_AndCallUpdateOfEfRepo()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Order());

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

            var passedModel = new OrderModel()
            {
                Id = 1,
                LabourPrice = 20,
                ProblemDescription = "description",
                Solution = "solution",
                Status = Status.Parts,
                WarrantyStatus = WarrantyStatus.No
            };

            // Act
            testedService.Update(passedModel);

            // Assert
            mockedOrderRepo.Verify(
                re => re.Update(It.Is<Order>(
                o => o.Id == 0
              && o.LabourPrice == passedModel.LabourPrice
              && o.ProblemDescription == passedModel.ProblemDescription
              && o.Solution == passedModel.Solution
              && o.Status == passedModel.Status
              && o.WarrantyStatus == passedModel.WarrantyStatus)), Times.Once);
        }

        [TestMethod]
        public void Call_SaveChanges()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Order());

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

            var passedModel = new OrderModel()
            {
                Id = 1,
                LabourPrice = 20,
                ProblemDescription = "description",
                Solution = "solution",
                Status = Status.Parts,
                WarrantyStatus = WarrantyStatus.No
            };

            // Act
            testedService.Update(passedModel);

            // Assert
            mockedSaveChagesRepo.Verify(re => re.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Throw_IfOrderWithPassedIdIsNotFound()
        {
            // Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(r => r.GetById(It.IsAny<int>())).Throws<ArgumentException>();

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

            var passedModel = new OrderModel()
            {
                Id = 1
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => testedService.Update(passedModel));
        }
    }
}
