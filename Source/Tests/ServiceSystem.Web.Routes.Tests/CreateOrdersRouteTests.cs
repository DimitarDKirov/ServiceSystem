namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Controllers;

    [TestFixture]
    public class CreateOrdersRouteTests
    {
        [Test]
        public void CreateOrderShouldRouteCorrectly()
        {
            string url = "/Order/Create";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<OrderController>(c => c.Add());
        }

        [Test]
        public void AddOrderShouldBeOk()
        {
            string url = "/Order/Create/Add/";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<OrderController>(c => c.Add(null));
        }
    }
}
