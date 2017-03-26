using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.CustomerServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenAllParametersAreNotNull()
        {
            //Arrange
            var customersRepoMock = new Mock<IEfDbRepository<Customer>>();
            var mappingServiceMock = new Mock<IMappingService>();

            //Act
            var testedService = new CustomerService(customersRepoMock.Object, mappingServiceMock.Object);

            //Assert
            Assert.IsNotNull(testedService);
        }

        [TestMethod]
        public void Throw_WhenCustomersRepoIsNull()
        {
            //Arrange
            var mappingServiceMock = new Mock<IMappingService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CustomerService(null, mappingServiceMock.Object));
        }

        [TestMethod]
        public void Throw_WhenmappingServiceIsNull()
        {
            //Arrange
            var customersRepoMock = new Mock<IEfDbRepository<Customer>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CustomerService(customersRepoMock.Object, null));
        }
    }
}
