namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Controllers;

    [TestFixture]
    public class OrderUpdateTests
    {
        [Test]
        public void UpdateTest()
        {
            string url = "/Order/Update/Edit/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<UpdateOrderController>(c => c.Edit(5));
        }

        [Test]
        public void UAssignTest()
        {
            string url = "/Order/Update/Assign/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<UpdateOrderController>(c => c.Assign(5));
        }

        [Test]
        public void DeliverTest()
        {
            string url = "/Order/Update/Deliver/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<UpdateOrderController>(c => c.Deliver(5));
        }
    }
}
