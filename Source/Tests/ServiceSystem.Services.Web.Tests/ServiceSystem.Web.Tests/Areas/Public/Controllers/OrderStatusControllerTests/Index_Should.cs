using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.Areas.Public.Controllers;
using ServiceSystem.Web.Areas.Public.Models.OrderStatus;
using TestStack.FluentMVCTesting;

namespace ServiceSystem.UnitTests.ServiceSystem.Web.Tests.Areas.Public.Controllers.OrderStatusControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestInitialize]
        public void ConfigAutomapper()
        {
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(new[] { typeof(OrderStatusController).Assembly });
        }

        [TestMethod]
        public void ReturnDefaultViewWhenCalleWithNoParameters()
        {
            //Arrange
            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnViewOrderStatus_WhenOrderIsFound()
        {
            //Arrange
            var publicCode = "1Iva";
            var orderStub = new OrderModel()
            {
                Id = 2,
                ProblemDescription = "problem",
                OrderPublicId = publicCode
            };

            var orderSearchViewModelStub = new OrderSearchViewModel()
            {
                UserInput = publicCode
            };

            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            mockedCoderService.Setup(cs => cs.Decode(It.IsAny<string>())).Returns(2);
            mockedOrderService.Setup(cs => cs.GetById(It.IsAny<int>())).Returns(orderStub);

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController
                .WithCallTo(c => c.Index(orderSearchViewModelStub))
                .ShouldRenderView("OrderStatus")
                .WithModel<OrderStatusViewModel>(m =>
                {
                    Assert.AreEqual(orderStub.Id, m.Id);
                    Assert.AreEqual(orderStub.ProblemDescription, m.ProblemDescription);
                });
        }

        [TestMethod]
        public void ReturnDefaultViewWithMessage_WhenPublicCodeCouldNotBeDecoded()
        {
            //Arrange
            var publicCode = "1Iva";

            var orderSearchViewModelStub = new OrderSearchViewModel()
            {
                UserInput = publicCode
            };

            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            mockedCoderService.Setup(cs => cs.Decode(It.IsAny<string>())).Throws<ArgumentException>();

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController
                .WithCallTo(c => c.Index(orderSearchViewModelStub))
                .ShouldRenderDefaultView()
                .WithModel<OrderSearchViewModel>(m =>
                {
                    StringAssert.Contains("Incorrect code", m.Result);
                });
        }

        [TestMethod]
        public void ReturnDefaultViewWithModelStateError_WhenOrderIsNotFound()
        {
            //Arrange
            var publicCode = "1Iva";
            var orderStub = new OrderModel()
            {
                Id = 2,
                ProblemDescription = "problem",
                OrderPublicId = publicCode
            };

            var orderSearchViewModelStub = new OrderSearchViewModel()
            {
                UserInput = publicCode
            };

            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            mockedCoderService.Setup(cs => cs.Decode(It.IsAny<string>())).Returns(2);
            mockedOrderService.Setup(cs => cs.GetById(It.IsAny<int>())).Returns(() => null);

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController
                .WithCallTo(c => c.Index(orderSearchViewModelStub))
                .ShouldRenderDefaultView()
                .WithModel<OrderSearchViewModel>(m =>
                {
                    StringAssert.Contains("Such order can not be found", m.Result);
                });
        }

        [TestMethod]
        public void ReturnDefaultViewWithModelStateError_WhenOrderIsNotFoundButStoredPublicCodeIsDifferent()
        {
            //Arrange
            var publicCodeStored = "1Iva";
            var publicCodeSent = "2Iva";
            var orderStub = new OrderModel()
            {
                Id = 2,
                ProblemDescription = "problem",
                OrderPublicId = publicCodeStored
            };

            var orderSearchViewModelStub = new OrderSearchViewModel()
            {
                UserInput = publicCodeSent
            };

            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            mockedCoderService.Setup(cs => cs.Decode(It.IsAny<string>())).Returns(5);
            mockedOrderService.Setup(cs => cs.GetById(It.IsAny<int>())).Returns(orderStub);

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController
                .WithCallTo(c => c.Index(orderSearchViewModelStub))
                .ShouldRenderDefaultView()
                .WithModel<OrderSearchViewModel>(m =>
                {
                    StringAssert.Contains("Incorrect code", m.Result);
                });
        }

        [TestMethod]
        public void ReturnDefaultView_WhenModelIsNotValid()
        {
            //Arrange
            var publicCode = "1Iva";
            var orderStub = new OrderModel()
            {
                Id = 2,
                ProblemDescription = "problem",
                OrderPublicId = publicCode
            };

            var orderSearchViewModelStub = new OrderSearchViewModel()
            {
                UserInput = publicCode
            };

            var mockedCoderService = new Mock<IPublicCodeProvider>();
            var mockedOrderService = new Mock<IOrderService>();

            mockedCoderService.Setup(cs => cs.Decode(It.IsAny<string>())).Returns(2);
            mockedOrderService.Setup(cs => cs.GetById(It.IsAny<int>())).Returns(orderStub);

            var testedController = new OrderStatusController(mockedCoderService.Object, mockedOrderService.Object);

            //Act & Assert
            testedController
                .WithModelErrors()
                .WithCallTo(c => c.Index(orderSearchViewModelStub))
                .ShouldRenderDefaultView()
                .WithModel<OrderSearchViewModel>(m =>
                {
                    Assert.AreEqual(orderSearchViewModelStub.UserInput, m.UserInput);
                });
        }
    }
}
