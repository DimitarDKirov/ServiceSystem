using System.Web.Mvc;
using System.Web.Routing;

namespace ServiceSystem.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            // routes.MapRoute(
            //    name: "OrderGrid",
            //    url: "Order/Grid/{action}",
            //    defaults: new { controller = "OrdersGrid", action = "Index" });
            // routes.MapRoute(
            //    name: "OrderCreate",
            //    url: "Order/Create/{action}",
            //    defaults: new { controller = "CreateOrder", action = "Index" });
            // routes.MapRoute(
            //    name: "OrderList",
            //    url: "Order/List/{action}",
            //    defaults: new { controller = "ListOrders", action = "Pending" });
            // routes.MapRoute(
            //   name: "OrderUpdate",
            //   url: "Order/Update/{action}/{id}",
            //   defaults: new { controller = "UpdateOrder", action = "Edit" });
            // routes.MapRoute(
            //   name: "OrderView",
            //   url: "Order/View/{action}/{id}",
            //   defaults: new { controller = "ViewOrder", action = "Details" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
