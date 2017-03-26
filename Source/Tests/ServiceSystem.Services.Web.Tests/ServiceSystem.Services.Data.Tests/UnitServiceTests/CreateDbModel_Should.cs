using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.UnitTests.ServiceSystem.Services.Data.Tests.UnitServiceTests
{
    [TestClass]
    public class CreateDbModel_Should
    {
        [TestMethod]
        public void CallBrandService_WithThePassedStringForBrandName()
        {
            // Arrange
            var unitModelStub = new UnitModel();
            var unitStub = new Unit();
            var brandNameStub = "brand";
            var mockedBrandsService = new Mock<IBrandsService>();
            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Unit>(It.IsAny<UnitModel>())).Returns(unitStub);

            var testedService = new UnitService(mockedBrandsService.Object, mockedMappingService.Object);

            //Act
            testedService.CreateDbModel(unitModelStub, brandNameStub);

            //Assert
            mockedBrandsService.Verify(bs => bs.CreateDbModel(It.Is<string>(s => s == brandNameStub)), Times.Once);
        }

        [TestMethod]
        public void MapPassedUnitModel_ToModel()
        {
            // Arrange
            var unitStub = new Unit();
            var unitModelStub = new UnitModel()
            {
                Brand = "brand",
                CategoryId = 1,
                Model = "model",
                SerialNumber = "123"
            };

            var brandNameStub = "brand";
            var mockedBrandsService = new Mock<IBrandsService>();
            var mockedMappingService = new Mock<IMappingService>(); mockedMappingService.Setup(ms => ms.Map<Unit>(It.IsAny<UnitModel>())).Returns(unitStub);

            var testedService = new UnitService(mockedBrandsService.Object, mockedMappingService.Object);


            //Act
            testedService.CreateDbModel(unitModelStub, brandNameStub);

            //Assert
            mockedMappingService.Verify(ms => ms.Map<Unit>(It.Is<UnitModel>(s => s == unitModelStub)), Times.Once);
        }

        [TestMethod]
        public void SetBrandAndReturnUnit()
        {
            // Arrange
            var brandStub = new Brand()
            {
                Id = 1,
                Name = "brand"
            };

            var unitStub = new Unit()
            {
                CategoryId = 1,
                Model = "model",
                SerialNumber = "123"
            };
            var unitModelStub = new UnitModel();

            var brandNameStub = "brand";
            var mockedBrandsService = new Mock<IBrandsService>();
            mockedBrandsService.Setup(bs => bs.CreateDbModel(It.IsAny<string>())).Returns(brandStub);

            var mockedMappingService = new Mock<IMappingService>();
            mockedMappingService.Setup(ms => ms.Map<Unit>(It.IsAny<UnitModel>())).Returns(unitStub);

            var testedService = new UnitService(mockedBrandsService.Object, mockedMappingService.Object);

            //Act
            var result = testedService.CreateDbModel(unitModelStub, brandNameStub);

            //Assert
            Assert.AreSame(brandStub, result.Brand);
            Assert.AreEqual(unitStub.CategoryId, result.CategoryId);
            Assert.AreEqual(unitStub.Model, result.Model);
            Assert.AreEqual(unitStub.SerialNumber, result.SerialNumber);
        }
    }
}
