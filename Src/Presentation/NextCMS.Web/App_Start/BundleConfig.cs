using System.Web;
using System.Web.Optimization;

namespace NextCMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862

            //TODO:
            BundleTable.EnableOptimizations = false;

            //公用
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Administration/Content/assets/plugins/jquery-migrate-1.2.1.min.js"));

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
                      "~/Content/global.css",
                      "~/Content/site.css",
                      "~/Content/site.response.css"
                      ));

            //后台管理系统

            //CSS 样式
            bundles.Add(new StyleBundle("~/bundles/admin/css").Include(
                       //全局样式
                      "~/Administration/Content/assets/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Administration/Content/assets/plugins/bootstrap/css/bootstrap.min.css",
                      //表单美化
                      "~/Administration/Content/assets/plugins/uniform/css/uniform.default.css",
                      //主题样式
                      "~/Administration/Content/assets/css/style-metronic.css",
                      "~/Administration/Content/assets/css/style.css",
                      "~/Administration/Content/assets/css/style-responsive.css",
                      "~/Administration/Content/assets/css/admin.main.css",
                      "~/Administration/Content/assets/css/admin.main-responsive.css",
                      "~/Administration/Content/assets/css/plugins.css",
                      "~/Administration/Content/assets/css/pages/tasks.css",
                      "~/Administration/Content/assets/css/custom.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/admin/grid/css").Include(
                      "~/Administration/Content/assets/plugins/select2/select2_metro.css",
                      "~/Administration/Content/assets/plugins/data-tables/DT_bootstrap.css"
                      ));

            //JS 脚本
            bundles.Add(new ScriptBundle("~/bundles/admin/js").Include(
                        //IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip
                        "~/Administration/Content/assets/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                        "~/Administration/Content/assets/plugins/bootstrap/js/bootstrap.min.js",
                        "~/Administration/Content/assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js",
                        "~/Administration/Content/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Administration/Content/assets/plugins/jquery.blockui.min.js",
                        "~/Administration/Content/assets/plugins/jquery.cokie.min.js",
                        "~/Administration/Content/assets/plugins/uniform/jquery.uniform.min.js",
                        "~/Administration/Content/assets/scripts/app.js",
                        "~/Administration/Content/assets/scripts/admin.main.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/admin/grid/js").Include(
                       "~/Administration/Content/assets/plugins/select2/select2.min.js",
                       "~/Administration/Content/assets/plugins/data-tables/jquery.dataTables.js",
                       "~/Administration/Content/assets/plugins/data-tables/jquery.dataTables.AjaxSource.min.js",
                       "~/Administration/Content/assets/plugins/data-tables/DT_bootstrap.js",
                       "~/Administration/Content/assets/scripts/table-managed.js"
                       ));



            //前台网站
            
        }
    }
}
