using System.Web.Optimization;

namespace ESN3.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                    .Include("~/Content/site.css")  /* не перепутайте порядок */
                    .Include("~/Content/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                     "~/Scripts/jquery-ui-1.*"));

            bundles.Add(new StyleBundle("~/Content/css/jqueryui")
                   .Include("~/Content/jquery-ui-1*"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-tooltip").Include(
                        "~/Scripts/bootstrap-tooltip.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-typeahead").Include(
                        "~/Scripts/bootstrap-typeahead.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery.unobtrusive-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

        }
    }
}