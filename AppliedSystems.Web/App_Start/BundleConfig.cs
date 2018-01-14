using System.Web;
using System.Web.Optimization;

namespace AppliedSystems.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/angular/modules").Include(
                "~/AngularJs/Ng.Module.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/angular/controllers")
                .IncludeDirectory("~/AngularJs/Controllers", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/scripts/angular/components").Include(
                "~/AngularJs/Components/as.input.js",
                "~/AngularJs/Components/as.date.js",
                "~/AngularJs/Components/as.select.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Styles/AppliedSystems.css",
                      "~/Content/Styles/Components/Component.css"));
        }
    }
}
