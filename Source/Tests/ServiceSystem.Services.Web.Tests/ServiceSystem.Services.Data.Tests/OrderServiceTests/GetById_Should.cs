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
    public class GetById_Should
    {
        [TestMethod]
        public void ReturnOrderModelWithThePassedId()
        {
            //Arrange
            var searchedId = 2;
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

            var orderStub = new Order()
            {
                Id = searchedId
            };

            var orderModelStub = new OrderModel()
            {
                Id = searchedId
            };

            mockedOrderRepo.Setup(or => or.GetById(It.Is<int>(id => id == searchedId))).Returns(orderStub);
            mockedMappingService.Setup(map => map.Map<OrderModel>(It.IsAny<Order>())).Returns(orderModelStub);

            //Act
            var result = testedService.GetById(searchedId);

            //Assert
            mockedOrderRepo.Verify(mr => mr.GetById(It.Is<int>(s => s == searchedId)), Times.Once);
            mockedMappingService.Verify(m => m.Map<OrderModel>(It.Is<Order>(o => o == orderStub)), Times.Once);
            Assert.AreSame(orderModelStub, result);
        }
    }
}
