namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Controllers;

    [TestFixture]
    public class ListOrderRouteTests
    {
        [Test]
        public void IndexTest()
        {
            string url = "/Order/List";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<ListOrdersController>(c => c.Pending(null));
        }
    }
}
