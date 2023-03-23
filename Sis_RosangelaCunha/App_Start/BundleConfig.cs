using System.Web;
using System.Web.Optimization;

namespace Sis_RosangelaCunha
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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
                      "~/Content/site.css"));



            bundles.Add(new ScriptBundle("~/bundles/dttables").Include(
               "~/Scripts/datatables/jquery.dataTables.min.js"
               , "~/Scripts/datatables/dataTables.bootstrap.js"
               , "~/Scripts/datatables/dataTables.buttons.min.js"
               , "~/Scripts/datatables/buttons.bootstrap.min.js"
               , "~/Scripts/datatables/jszip.min.js"
               , "~/Scripts/datatables/pdfmake.min.js"
               , "~/Scripts/datatables/vfs_fonts.js"
               , "~/Scripts/datatables/buttons.html5.min.js"
               , "~/Scripts/datatables/buttons.print.min.js"
               , "~/Scripts/datatables/dataTables.fixedHeader.min.js"
               , "~/Scripts/datatables/dataTables.keyTable.min.js"
               , "~/Scripts/datatables/dataTables.responsive.min.js"
               , "~/Scripts/datatables/responsive.bootstrap.min.js"
               , "~/Scripts/datatables/dataTables.scroller.min.js"));

        }
    }
}
