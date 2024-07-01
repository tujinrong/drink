using System.Web;
using System.Web.Optimization;

namespace WebEvaluation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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

            
            //common
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                            "~/Scripts/datepicker/bootstrap-datepicker.js",
                            "~/Scripts/datepicker/bootstrap-datepicker.ja.js",
                            "~/Scripts/datetimepicker/bootstrap-datetimepicker.js",
                            "~/Scripts/datetimepicker/bootstrap-datetimepicker.ja.js",
                            "~/Scripts/toastr/toastr.js",
                            "~/Scripts/toastr/glimpse.js",
                            "~/Scripts/toastr/glimpse.toastr.js",
                            "~/Scripts/Common/select2.min.js",
                            "~/Scripts/Common/icheck.min.js",
                            "~/Scripts/jquery.webui-popover.js",
                             "~/Scripts/Common/SMAT.Controls.js",
                            "~/Scripts/Common/SMAT.Service.js",
                            "~/Scripts/Common/SMAT.util.js",
                            "~/Scripts/Common/referConfig.js"));

            bundles.Add(new StyleBundle("~/Content/Common/css").Include(
                        "~/Content/toastr/toastr.css",
                        "~/Content/datepicker/datepicker3.css",
                        "~/Content/datetimepicker/bootstrap-datetimepicker.css",
                        "~/Content/jquery.webui-popover.css",
                        "~/Content/select2/select2.css",
                        "~/Content/select2/select2-bootstrap.css",
                         "~/Content/icheckSkins/square/blue.css")); 
        }
    }
}
