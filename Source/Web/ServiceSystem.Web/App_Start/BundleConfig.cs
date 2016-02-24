namespace ServiceSystem.Web
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/Kendo/kendo.all.min.js",
                        // "~/Scripts/Kendo/kendo.autocomplete.min.js",
                        // "~/Scripts/Kendo/kendo.datepicker.min.js",
                        // "~/Scripts/Kendo/kendo.datetimepicker.min.js",
                        // "~/Scripts/Kendo/kendo.grid.min.js",
                        "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/kendo-css").Include(
                    "~/Content/Kendo/kendo.common.min.css",
                    "~/Content/Kendo/kendo.default.min.css"));
        }
    }
}
