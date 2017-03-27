using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceSystem.Web.Areas.Administration.Controllers;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Administration.Controllers.CategoriesControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnAnInstance_WhenCategoriesServiceIsNotNull()
        {
            //Assert
            var mockedCategoryService = new Mock<ICategoryService>();

            //Act
            var testedController = new CategoriesController(mockedCategoryService.Object);

            //Assert
            Assert.IsNotNull(testedController);
        }

        [TestMethod]
        public void Throw_IfCategoriesServiceIsNull()
        {
            //Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoriesController(null));
        }
    }
}
