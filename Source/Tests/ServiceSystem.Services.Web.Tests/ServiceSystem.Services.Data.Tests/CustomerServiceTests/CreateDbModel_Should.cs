using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.CustomerServiceTests
{
    [TestClass]
    public class CreateDbModel_Should
    {
        [TestMethod]
        public void CallMappingServiceWithPassedModel()
        {
            // Arrange
            var customersRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();
            var testedService = new CustomerService(customersRepoMock.Object, mappingServiceMock.Object);
            var customerModelStub = new CustomerModel()
            {
                Id = 1,
                Email = "some@some.bg",
                Name = "test model",
                Phone = "08111"
            };

            // Act
            testedService.CreateDbModel(customerModelStub);

            // Assert
            mappingServiceMock.Verify(ms => ms.Map<Customer>(It.Is<CustomerModel>(m => m == customerModelStub)), Times.Once);
        }

        [TestMethod]
        public void ReturnResultFromMapping()
        {
            // Arrange
            var customersRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();
            var testedService = new CustomerService(customersRepoMock.Object, mappingServiceMock.Object);

            var customerModel = new CustomerModel();
            var customerStub = new Customer()
            {
                Id = 1,
                Email = "some@some.bg",
                Name = "test model",
                Phone = "08111"
            };
            mappingServiceMock.Setup(ms => ms.Map<Customer>(It.IsAny<CustomerModel>())).Returns(customerStub);

            // Act
            var result = testedService.CreateDbModel(customerModel);

            // Assert
            Assert.AreSame(customerStub, result);
        }
    }
}
