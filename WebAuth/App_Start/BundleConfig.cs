using System.Web;
using System.Web.Optimization;

namespace WebAuth
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                           "~/Scripts/pnotify.core.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
           "~/Content/bootstrap.css",
          "~/Content/site.css",
          "~/Content/bootstrap-responsive.css", //新增，用于管理后台菜单
          "~/Content/pnotify.core.css"));


            bundles.Add(new StyleBundle("~/Content/jqueryuiall").Include(
           "~/Content/themes/base/all.css"));

            //jquery-ui
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                 "~/Scripts/jquery-ui-{version}.js"
                ));
            //<!-- Ignite UI Required Combined CSS Files -->
            bundles.Add(new StyleBundle("~/IgniteUI/css").Include(
                "~/igniteui/css/themes/metro/infragistics.theme.css",
                "~/igniteui/css/structure/infragistics.css"
                ));
            //<!-- Ignite UI Required Combined JavaScript Files -->
            bundles.Add(new ScriptBundle("~/IgniteUI/js").Include(
                "~/igniteui/js/infragistics.core.js",
                "~/igniteui/js/infragistics.dv.js",
                "~/igniteui/js/infragistics.loader.js",
                "~/igniteui/js/infragistics.lob.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/project.js")
                .IncludeDirectory("~/Scripts/Project", "*.js", false));
        }
    }
}
