using System.Web;
using System.Web.Optimization;

namespace VlogRoom.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/Kendo/kendo.web.min.js",
                        "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                        "~/Scripts/Template/responsiveslides.min.js",
                        "~/Scripts/toastr.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.min.css",
                       "~/Content/Site.css",
                       "~/Content/fontawesome/font-awesome.css",
                       "~/Content/toastr.min.css"));

            bundles.Add(new StyleBundle("~/Content/template-css").Include(
                       "~/Content/Template/dashboard.css",
                       "~/Content/Template/template.css"));

            bundles.Add(new StyleBundle("~/Content/kendo-css").Include(
                      "~/Content/Kendo/kendo.black.min.css",
                      "~/Content/Kendo/kendo.commin.min.css"));
        }
    }
}
