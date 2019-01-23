using System.Web;
using System.Web.Optimization;

namespace FortyLife
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                "~/Scripts/EnableTooltip.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Bootstrap/bootstrap.min.css",
                "~/Content/open-iconic-bootstrap.min.css",
                "~/Content/mana.min.css",
                "~/Content/keyrune.min.css",
                "~/Content/Site.css",
                "~/Content/ColorPie.css",
                "~/Content/BlackLotus.css",
                "~/Content/CardFlip.css"));
        }
    }
}
