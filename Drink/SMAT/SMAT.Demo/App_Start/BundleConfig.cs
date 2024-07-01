using System.Web;
using System.Web.Optimization;

namespace ASMAT.Demo
{
    public class BundleConfig
    {
        // バンドルの詳細については、http://go.microsoft.com/fwlink/?LinkId=301862  を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/SMAT.UI/Scripts/jquery.min.js",
                        "~/SMAT.UI/Scripts/jquery.ba-throttle-debounce.min.js"));


            bundles.Add(new StyleBundle("~/SMAT.UI/Styles/css").Include(
                      "~/SMAT.UI/Styles/bootstrap.css",
                      "~/SMAT.UI/Styles/asmat.common.animate.css",
                      "~/SMAT.UI/Styles/asmat.common.icon.css",
                      "~/SMAT.UI/Styles/asmat.common-material.min.css",
                      "~/SMAT.UI/Styles/asmat.material.min.css",
                      "~/SMAT.UI/Styles/asmat.app.css",
                      "~/ASMAT.UI/Styles/css/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/SMAT.UI/Scripts/js/bootstrap.js",
                      "~/SMAT.UI/Scripts/js/asmat.app.js",
                      "~/SMAT.UI/Scripts/js/slimscroll/jquery.slimscroll.min.js",
                      "~/SMAT.UI/Scripts/js/asmat.app.plugin.js",
                      "~/SMAT.UI/Scripts/asmat.all.min.js",
                      "~/SMAT.UI/Scripts/smat.core.js",
                      "~/SMAT.UI/Scripts/smat.service.js",
                      "~/SMAT.UI/Scripts/smat.ui.js",
                      "~/SMAT.UI/Scripts/smat.ui.*"));

            // デバッグを行うには EnableOptimizations を false に設定します。詳細については、
            // http://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
            BundleTable.EnableOptimizations = true;
        }
    }
}
