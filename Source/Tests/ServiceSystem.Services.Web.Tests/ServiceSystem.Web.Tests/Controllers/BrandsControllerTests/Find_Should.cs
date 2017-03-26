using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.BrandsControllerTests
{
    [TestClass]
    public class Find_Should
    {
        [TestMethod]
        public void ReturnArrayOfStringsAsJson()
        {
            // Arrange
            string searchedBrand = "brand";
            var brands = new[] { "Brand1", "Brand2" };
            var brandsServiceMock = new Mock<IBrandsService>();
            brandsServiceMock.Setup(bs => bs.FindByName(It.IsAny<string>())).Returns(brands);

            var testedController = new BrandsController(brandsServiceMock.Object);

            // Act & Assert
            testedController
                .WithCallTo(bc => bc.Find(searchedBrand))
                .ShouldReturnJson(result => CollectionAssert.AreEquivalent(brands, result));
        }
    }
}
