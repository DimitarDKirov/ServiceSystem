using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.BrandsServiceTests
{
    [TestClass]
    public class CreateDbModel_Shoud
    {
        [TestMethod]
        public void ReturnExistingModel_WhenModelWithTheSameNameExists()
        {
            // Arrange
            var searchedName = "test name";
            var existingModelsList = new List<Brand>()
            {
                new Brand()
                {
                    Id = 1,
                    Name = searchedName
                }
            };
            var mockedEfRepo = new Mock<IEfDbRepository<Brand>>();
            mockedEfRepo.Setup(m => m.All()).Returns(existingModelsList.AsQueryable());
            var testedService = new BrandsService(mockedEfRepo.Object);

            // Act
            var result = testedService.CreateDbModel(searchedName);

            // Assert
            Assert.AreSame(existingModelsList.Single(), result);
        }

        [TestMethod]
        public void ReturnNewModelWithSetPropertyName_WhenModelWithSerachedNameDoesNotExists()
        {
            // Arrange
            var searchedName = "test name";
            var existingModelsList = new List<Brand>()
            {
                new Brand()
                {
                    Id = 1,
                    Name = "some name"
                }
            };
            var mockedEfRepo = new Mock<IEfDbRepository<Brand>>();
            mockedEfRepo.Setup(m => m.All()).Returns(existingModelsList.AsQueryable());
            var testedService = new BrandsService(mockedEfRepo.Object);

            // Act
            var result = testedService.CreateDbModel(searchedName);

            // Assert
            Assert.AreNotSame(existingModelsList.Single(), result);
            Assert.AreEqual(searchedName, result.Name);
        }
    }
}
