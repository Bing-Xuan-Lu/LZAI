using System.Web;
using System.Web.Optimization;

namespace LZAIMonitor
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery-{version}.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js"
            ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/all.css",
                "~/Content/v4-shims.css",
                "~/Content/themes/base/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Content/MasterCss").Include(
                "~/Content/form.css",
                "~/Content/print.css",
                "~/Content/loader.css"));

            bundles.Add(new StyleBundle("~/Content/BootstrapTableCss").Include(
                "~/Content/BootstrapTable/all.css",
                "~/Content/BootstrapTable/bootstrap-table.min.css",
                "~/Content/BootstrapTable/default.min.css",
                "~/Content/BootstrapTable/hint.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/BoostrapTableJs").Include(
                "~/Scripts/BootstrapTable/bootstrap-table.min.js",
                "~/Scripts/BootstrapTable/locale-All.js"
            ));

            bundles.Add(new StyleBundle("~/Content/DataTableCss").Include(
                "~/Content/dataTables.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/bundles/DataTableJs").Include(
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/dataTable.js",
                "~/Scripts/dataTables.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
                "~/Scripts/Common/VerifyFormat.js",
                "~/Scripts/Common/VerifyType.js",
                "~/Scripts/Common/Dialog.js",
                "~/Scripts/Common/Data.js",//依賴Jquery
                "~/Scripts/Common/Autocomplete.js",
                "~/Scripts/Common/Format.js",
                "~/Scripts/Common/Utility.js",
                "~/Scripts/Common/CityTown.js",
                "~/Scripts/Common/WasteCode.js",
                "~/Scripts/Common/SweetDialog.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery-ui-{version}.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Select2").Include(
                "~/Scripts/select2.js"
            ));

            bundles.Add(new StyleBundle("~/Content/sweetalert2Css").Include(
                "~/Content/Sweetalert2/sweetalert2.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2Js").Include(
              "~/Scripts/Sweetalert2/sweetalert2.js"
          ));
            bundles.Add(new StyleBundle("~/Content/Select2").Include(
                "~/Content/css/select2.css",
                "~/Content/css/select2-bootstrap4.css"
            ));

        }
    }
}
