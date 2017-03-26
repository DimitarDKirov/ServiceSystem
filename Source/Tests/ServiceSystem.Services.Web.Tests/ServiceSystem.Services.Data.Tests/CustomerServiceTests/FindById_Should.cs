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
    public class FindById_Should
    {
        public void SearchTheRepo_WithPassedId()
        {
            // Arrange
            int searchedId = 5;
            var customerssRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();
            var testedService = new CustomerService(customerssRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.FindById(searchedId);

            // Assert
            customerssRepoMock.Verify(cr => cr.GetById(It.Is<int>(s => s == searchedId)), Times.Once);
        }

        [TestMethod]
        public void SearchTheRepo_AndMapToCustomerModel()
        {
            // Arrange
            int searchedId = 5;
            var customerStub = new Customer()
            {
                Id = searchedId,
                Name = "Ivan",
                Email = "iv@abv.bg",
                Phone = "0811"
            };

            var customerssRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();

            customerssRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(customerStub);

            var testedService = new CustomerService(customerssRepoMock.Object, mappingServiceMock.Object);

            // Act
            testedService.FindById(searchedId);

            // Assert
            mappingServiceMock.Verify(ms => ms.Map<CustomerModel>(It.Is<Customer>(c => c == customerStub)), Times.Once);
        }

        [TestMethod]
        public void ReturnCustomerModel()
        {
            // Arrange
            int searchedId = 5;
            var customerStub = new Customer()
            {
                Id = searchedId,
                Name = "Ivan",
                Email = "iv@abv.bg",
                Phone = "0811"
            };
            var customerModelStub = new CustomerModel()
            {
                Id = searchedId,
                Name = "Ivan",
                Email = "iv@abv.bg",
                Phone = "0811"
            };

            var customerssRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();

            mappingServiceMock.Setup(ms => ms.Map<CustomerModel>(It.IsAny<Customer>())).Returns(customerModelStub);

            var testedService = new CustomerService(customerssRepoMock.Object, mappingServiceMock.Object);

            // Act
            var result = testedService.FindById(searchedId);

            // Assert
            Assert.AreSame(customerModelStub, result);
        }
    }
}
