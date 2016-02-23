namespace ServiceSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "OrderList",
                url: "Order/List/{action}",
                defaults: new { controller = "ListOrders", action = "Pending" });
            routes.MapRoute(
               name: "OrderUpdate",
               url: "Order/Update/{action}/{id}",
               defaults: new { controller = "UpdateOrder", action = "Pending" });
            routes.MapRoute(
               name: "OrderView",
               url: "Order/View/{action}/{id}",
               defaults: new { controller = "ViewOrder", action = "Details" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
