using System.Web;
using System.Web.Optimization;

namespace DrinkService
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
                      "~/SMAT.UI/Styles/asmat.app.css"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/SMAT.UI/Scripts/js/bootstrap.js",
                      "~/SMAT.UI/Scripts/js/asmat.app.js",
                      "~/SMAT.UI/Scripts/js/slimscroll/jquery.slimscroll.min.js",
                      "~/SMAT.UI/Scripts/js/asmat.app.plugin.js",
                      "~/SMAT.UI/Scripts/asmat.all.min.js",
                      "~/SMAT.UI/Scripts/smat.core.js",
                      "~/SMAT.UI/Scripts/smat.service.js",
                      "~/SMAT.UI/Scripts/smat.ui.js",

                      "~/SMAT.UI/Scripts/smat.ui.Button.js",
                      "~/SMAT.UI/Scripts/smat.ui.DatePicker.js",
                      "~/SMAT.UI/Scripts/smat.ui.DateTimePicker.js",
                      "~/SMAT.UI/Scripts/smat.ui.DropDownList.js",
                      "~/SMAT.UI/Scripts/smat.ui.Form.js",
                      "~/SMAT.UI/Scripts/smat.ui.Grid.js",
                      "~/SMAT.UI/Scripts/smat.ui.NumericTextBox.js",
                      "~/SMAT.UI/Scripts/smat.ui.Refer.js",
                      "~/SMAT.UI/Scripts/smat.ui.RadioButton.js",
                      "~/SMAT.UI/Scripts/smat.ui.Pager.js",
                      "~/SMAT.UI/Scripts/smat.ui.TextBox.js",
                      "~/SMAT.UI/Scripts/cultures/smat.culture.ja.min.js",
                      
                      "~/Scripts/smat.config.js",

                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.js",
                      
                      "~/SMAT.UI/Scripts/dynamics/template/smat.dynamics.template.js",
                      "~/SMAT.UI/Scripts/dynamics/template/smat.dynamics.template.SimpleSearch.js",

                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.formList.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.designer.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.tools.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.tools.editer.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Page.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Section.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Form.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Field.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.ToolBar.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Button.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Grid.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Line.js",
                      "~/SMAT.UI/Scripts/dynamics/smat.dynamics.controls.Pager.js"
                      ));


            BundleTable.EnableOptimizations = true;
        }
    }
}
