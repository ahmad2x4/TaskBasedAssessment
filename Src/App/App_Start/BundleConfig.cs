using System.Web.Optimization;

namespace App
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/angular", "//ajax.googleapis.com/ajax/libs/angularjs/1.2.21/angular.min.js")
                .Include("~/scripts/angular/angular.js")
                .Include("~/scripts/angular/angular-cookies.js")
                .Include("~/scripts/angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/js/jquery", "//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js")
                .Include("~/scripts/jquery/jquery-1.9.0.js"));

            bundles.Add(new ScriptBundle("~/js/bootstrap", "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js")
                .Include("~/scripts/bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/angular/angular-route.js",
                "~/scripts/app.js",
                "~/scripts/constants.js",
                "~/scripts/filters.js",
                "~/scripts/services.js",
                "~/scripts/factory.js",
                "~/scripts/directives.js",
                "~/scripts/controllers.js"));

            bundles.Add(new StyleBundle("~/css/app").Include("~/Content/app.css"));
        }
    }
}