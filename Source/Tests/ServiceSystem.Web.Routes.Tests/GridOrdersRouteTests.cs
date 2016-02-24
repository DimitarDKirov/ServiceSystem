namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Controllers;

    [TestFixture]
    public class GridOrdersRouteTests
    {
        [Test]
        public void GridOrderTest()
        {
            string url = "/Order/Grid";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<OrdersGridController>(c => c.Index());
        }
    }
}
