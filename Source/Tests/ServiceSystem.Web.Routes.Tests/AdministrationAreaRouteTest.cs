namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using ServiceSystem.Web;
    using ServiceSystem.Web.Areas.Administration.Controllers;

    [TestFixture]
    public class AdministrationAreaRouteTest
    {
        [Test]
        public void TestMainPage()
        {
            string url = "Administration/Administration/Index";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<AdministrationController>(c => c.Index());
        }

        [Test]
        public void TestEditCategoriesById()
        {
            string url = "Administration/Categories/Edit/6";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<CategoriesController>(c => c.Edit(6));
        }
    }
}
