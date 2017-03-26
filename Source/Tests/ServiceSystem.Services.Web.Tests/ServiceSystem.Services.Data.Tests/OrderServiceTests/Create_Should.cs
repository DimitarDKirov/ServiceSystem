using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.OrderServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void CallCustomerServiceWithCustomerModel()
        {
            //Arrange
            var customerModelStub = new CustomerModel()
            {
                Name = "name"
            };
            var unitModelStub = new UnitModel();
            var orderModelStub = new OrderModel()
            {
                Customer = customerModelStub,
                Unit = unitModelStub
            };

            var customerStub = new Customer()
            {
                Name = "name"
            };
            var unitStub = new Unit();
            var orderStub = new Order()
            {
                Customer = customerStub
            };

            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(mo => mo.Add(It.IsAny<Order>())).Returns(orderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Order>(It.IsAny<OrderModel>())).Returns(orderStub);

            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            mockedCustomerService.Setup(mc => mc.CreateDbModel(It.IsAny<CustomerModel>())).Returns(customerStub);
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
               mockedOrderRepo.Object,
               mockedSaveChagesRepo.Object,
               mockedMappingService.Object,
               mockedUnitService.Object,
               mockedCustomerService.Object,
               mockedPublicCodeProvider.Object);

            //Act
            testedService.Create(orderModelStub);

            //Assert
            mockedCustomerService.Verify(cs => cs.CreateDbModel(It.Is<CustomerModel>(cm => cm == customerModelStub)), Times.Once);
        }

        [TestMethod]
        public void CallUnitServiceWithUnitModelAndBrandName()
        {
            //Arrange
            var brandName = "brand";
            var customerModelStub = new CustomerModel()
            {
                Name = "name"
            };
            var unitModelStub = new UnitModel()
            {
                Brand = brandName
            };
            var orderModelStub = new OrderModel()
            {
                Customer = customerModelStub,
                Unit = unitModelStub
            };

            var customerStub = new Customer()
            {
                Name = "name"
            };
            var unitStub = new Unit();
            var orderStub = new Order()
            {
                Customer = customerStub
            };

            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(mo => mo.Add(It.IsAny<Order>())).Returns(orderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Order>(It.IsAny<OrderModel>())).Returns(orderStub);

            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            mockedCustomerService.Setup(mc => mc.CreateDbModel(It.IsAny<CustomerModel>())).Returns(customerStub);
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
               mockedOrderRepo.Object,
               mockedSaveChagesRepo.Object,
               mockedMappingService.Object,
               mockedUnitService.Object,
               mockedCustomerService.Object,
               mockedPublicCodeProvider.Object);

            //Act
            testedService.Create(orderModelStub);

            //Assert
            mockedUnitService.Verify(us => us.CreateDbModel(It.Is<UnitModel>(um => um == unitModelStub), It.Is<string>(s => s == brandName)), Times.Once);
        }

        [TestMethod]
        public void CallEfRepositoryAddWithOrderModelAndStatusSetToPending()
        {
            //Arrange
            var brandName = "brand";
            var customerModelStub = new CustomerModel()
            {
                Name = "name"
            };
            var unitModelStub = new UnitModel()
            {
                Brand = brandName
            };
            var orderModelStub = new OrderModel()
            {
                Customer = customerModelStub,
                Unit = unitModelStub
            };

            var customerStub = new Customer()
            {
                Name = "name"
            };
            var unitStub = new Unit();
            var orderStub = new Order();

            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(mo => mo.Add(It.IsAny<Order>())).Returns(orderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Order>(It.IsAny<OrderModel>())).Returns(orderStub);

            var mockedUnitService = new Mock<IUnitService>();
            mockedUnitService.Setup(mu => mu.CreateDbModel(It.IsAny<UnitModel>(), It.IsAny<string>())).Returns(unitStub);

            var mockedCustomerService = new Mock<ICustomerService>();
            mockedCustomerService.Setup(mc => mc.CreateDbModel(It.IsAny<CustomerModel>())).Returns(customerStub);
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
               mockedOrderRepo.Object,
               mockedSaveChagesRepo.Object,
               mockedMappingService.Object,
               mockedUnitService.Object,
               mockedCustomerService.Object,
               mockedPublicCodeProvider.Object);

            //Act
            testedService.Create(orderModelStub);

            //Assert
            mockedOrderRepo.Verify(or => or.Add(It.Is<Order>(o => o == orderStub && o.Customer == customerStub && o.Unit == unitStub && o.Status == Status.Pending)));
        }

        [TestMethod]
        public void CallSaveChangesTwice()
        {
            //Arrange
            var brandName = "brand";
            var customerModelStub = new CustomerModel()
            {
                Name = "name"
            };
            var unitModelStub = new UnitModel()
            {
                Brand = brandName
            };
            var orderModelStub = new OrderModel()
            {
                Customer = customerModelStub,
                Unit = unitModelStub
            };

            var customerStub = new Customer()
            {
                Name = "name"
            };
            var unitStub = new Unit();
            var orderStub = new Order();

            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            mockedOrderRepo.Setup(mo => mo.Add(It.IsAny<Order>())).Returns(orderStub);

            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Order>(It.IsAny<OrderModel>())).Returns(orderStub);

            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            mockedCustomerService.Setup(mc => mc.CreateDbModel(It.IsAny<CustomerModel>())).Returns(customerStub);

            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            var testedService = new OrderService(
               mockedOrderRepo.Object,
               mockedSaveChagesRepo.Object,
               mockedMappingService.Object,
               mockedUnitService.Object,
               mockedCustomerService.Object,
               mockedPublicCodeProvider.Object);

            //Act
            testedService.Create(orderModelStub);

            //Assert
            mockedSaveChagesRepo.Verify(sc => sc.SaveChanges(), Times.Exactly(2));
        }

        [TestMethod]
        public void Throw_WhenOrderModelIsNull()
        {
            //Arrange
            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
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

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => testedService.Create(null));
        }
    }
}
