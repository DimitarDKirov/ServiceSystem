using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceSystem.Web.Controllers;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Controllers.BrandsControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_WhenBrandsServiceIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new BrandsController(null));
        }
    }
}
