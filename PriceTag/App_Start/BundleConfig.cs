using System.Web;
using System.Web.Optimization;

namespace PriceTag
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

            //Angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular/1.2.26/angular.js",
                 "~/Scripts/angular/1.2.26/angular-resource.js",
                 "~/Scripts/angular/1.2.26/angular-route.js",
                 "~/Scripts/angular/1.2.26/angular-sanitize.js",
                 "~/Scripts/angular/apps.js",
                 "~/Scripts/angular/api.js",
                 "~/Scripts/angular/service.js",
                 "~/Scripts/angular/directive.js",
                 "~/Scripts/angular/controller/listing.js",
                 "~/Scripts/angular/controller/enquiry.js",
                 "~/Scripts/angular/controller/account.js",
                 "~/Scripts/angular/controller/admin.js",
                 "~/Scripts/angular/3rd/angular-bootstrap-select.js",
                 "~/Scripts/angular/3rd/ui-bootstrap-tpls-0.6.0.js",
                 "~/Scripts/angular/3rd/loading-bar.js",
                 "~/Scripts/angular/3rd/angular-strap.js",
                 "~/Scripts/angular/3rd/angular-strap.tpl.js",
                 "~/Scripts/angular/3rd/angular-busy.js"
                 ));


            //File uploader
            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/Scripts/fileupload/vendor/jquery.ui.widget.js",
                "~/Scripts/fileupload/load-image.js",
                "~/Scripts/fileupload/jquery.iframe-transport.js",
                "~/Scripts/fileupload/jquery.fileupload.js",
                "~/Scripts/fileupload/jquery.fileupload-process.js",
                "~/Scripts/fileupload/jquery.fileupload-image.js",
                "~/Scripts/fileupload/jquery.fileupload-validate.js",
                "~/Scripts/fileupload/jquery.fileupload-angular.js",
                "~/Scripts/fileupload/app.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
