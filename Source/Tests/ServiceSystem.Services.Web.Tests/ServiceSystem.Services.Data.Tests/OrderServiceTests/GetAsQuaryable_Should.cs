using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping;
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
    public class GetAsQuaryable_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderService).Assembly });
        }

        [TestMethod]
        public void ReturnOrdersAsIQueryableOfOrderModels()
        {
            // Arrange
            var objects = new List<Order>()
            {
                new Order() {Id=1 },
                new Order() {Id=2 }
            };

            var mockedOrderRepo = new Mock<IEfDbRepository<Order>>();
            var mockedSaveChagesRepo = new Mock<IEfDbRepositorySaveChanges>();
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUnitService = new Mock<IUnitService>();
            var mockedCustomerService = new Mock<ICustomerService>();
            var mockedPublicCodeProvider = new Mock<IPublicCodeProvider>();

            mockedOrderRepo.Setup(o => o.All()).Returns(objects.AsQueryable());

            var testedService = new OrderService(
                mockedOrderRepo.Object,
                mockedSaveChagesRepo.Object,
                mockedMappingService.Object,
                mockedUnitService.Object,
                mockedCustomerService.Object,
                mockedPublicCodeProvider.Object);

            //Act
            var result = testedService.GetAsQuaryable();

            //Assert
            Assert.AreEqual(objects[0].Id, result.First().Id);
            Assert.AreEqual(objects[1].Id, result.Last().Id);
        }
    }
}
