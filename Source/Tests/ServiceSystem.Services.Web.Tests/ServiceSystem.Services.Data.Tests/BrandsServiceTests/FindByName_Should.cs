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
    public class FindByName_Should
    {
        [TestMethod]
        public void ReturnEnumerableOfStrings_WhichContainSearchedWord()
        {
            // Arrange
            var searchedWord = "and";
            var avaialbleBrands = new List<Brand>()
            {
                 new Brand()
                {
                    Name = "some text"
                },
                new Brand()
                {
                    Name = "brand 1"
                },
                 new Brand()
                {
                    Name = "brand 2"
                },
                 new Brand()
                {
                    Name = "smth"
                }
            };
            var mockedEfRepo = new Mock<IEfDbRepository<Brand>>();
            mockedEfRepo.Setup(m => m.All()).Returns(avaialbleBrands.AsQueryable());
            var testedService = new BrandsService(mockedEfRepo.Object);

            // Act
            var result = testedService.FindByName(searchedWord);

            // Assert
            Assert.AreEqual(avaialbleBrands[1].Name, result.First());
            Assert.AreEqual(avaialbleBrands[2].Name, result.Last());
        }

        [TestMethod]
        public void ReturnEnumerableOfStrings_WhenBrandsWithTheSameNameExistWithDifferentCase()
        {
            // Arrange
            var searchedWord = "aND";
            var avaialbleBrands = new List<Brand>()
            {
                new Brand()
                {
                    Name = "brand 1"
                }
            };
            var mockedEfRepo = new Mock<IEfDbRepository<Brand>>();
            mockedEfRepo.Setup(m => m.All()).Returns(avaialbleBrands.AsQueryable());
            var testedService = new BrandsService(mockedEfRepo.Object);

            // Act
            var result = testedService.FindByName(searchedWord);

            // Assert
            Assert.AreEqual(avaialbleBrands[0].Name, result.First());
        }

        [TestMethod]
        public void ReturnNull_WhenSearchedWordIsNull()
        {
            // Arrange
            var avaialbleBrands = new List<Brand>()
            {
                 new Brand()
                {
                    Name = "some text"
                },
                new Brand()
                {
                    Name = "brand 1"
                },
                 new Brand()
                {
                    Name = "brand 2"
                },
                 new Brand()
                {
                    Name = "smth"
                }
            };
            var mockedEfRepo = new Mock<IEfDbRepository<Brand>>();
            mockedEfRepo.Setup(m => m.All()).Returns(avaialbleBrands.AsQueryable());
            var testedService = new BrandsService(mockedEfRepo.Object);

            // Act
            var result = testedService.FindByName(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}
