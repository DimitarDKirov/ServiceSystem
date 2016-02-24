namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Controllers;

    [TestFixture]
    public class ViewOrderTest
    {
        [Test]
        public void DefaultRouteToViewOrderTest()
        {
            string url = "/Order/View/Details/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<ViewOrderController>(c => c.Details(5));
        }

        [Test]
        public void CustomerOrderViewTest()
        {
            string url = "/Order/View/AfterCreation/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<ViewOrderController>(c => c.AfterCreation(5));
        }

        [Test]
        public void CommonOrderViewTest()
        {
            string url = "/Order/View/CommonDetails/5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<ViewOrderController>(c => c.CommonDetails(5));
        }
    }
}
