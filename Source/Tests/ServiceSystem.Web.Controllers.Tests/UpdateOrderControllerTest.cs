namespace MvcTemplate.Web.Controllers.Tests
{
    using NUnit.Framework;
    using ServiceSystem.Data.Models;

    [TestFixture]
    public class UpdateOrderControllerTest
    {
        private Order order = new Order
        {
            Id = 1,
            ProblemDescription = "some decription",
            Status = Status.Pending,
            WarrantyStatus = WarrantyStatus.No,
            UserId = "1"
        };

        // [Test]
        // public void EditShouldredirectIfStatusDelivered()
        // {
        //    var autoMapperConfig = new AutoMapperConfig();
        //    autoMapperConfig.Execute(typeof(IOrderService).Assembly);
        //    var ordersServiceMock = new Mock<IOrderService>();
        //    ordersServiceMock.Setup(x => x.GetById(It.IsAny<int>()))
        //        .Returns(() => null);
        //    var controller = new UpdateOrderController(ordersServiceMock.Object);
        //    // controller.WithCallTo(x => x.Edit(1)).
        //    //   .ShouldRenderView("Edit");
        // }

        // [Test]
        // public void DeliverShouldWorkCorrectly()
        // {
        //    var autoMapperConfig = new AutoMapperConfig();
        //    autoMapperConfig.Execute(typeof(IOrderService).Assembly);
        //    var ordersServiceMock = new Mock<IOrderService>();
        //    ordersServiceMock.Setup(x => x.GetById(It.IsAny<int>()))
        //        .Returns(this.order);
        //    ordersServiceMock.Setup(x => x.Update(It.IsAny<Order>()))
        //       .Returns(this.order);
        //    var controller = new UpdateOrderController(ordersServiceMock.Object);
        //    // controller.WithCallTo(x => x.Deliver(1))
        //    //    .ShouldRedirectTo()
        // }
    }
}
