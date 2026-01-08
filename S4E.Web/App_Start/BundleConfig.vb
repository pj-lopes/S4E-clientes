Imports System.Web.Optimization

Public Module BundleConfig
    ' Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery/jquery-3.7.0.min.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery/jquery.validate*"))

        bundles.Add(New ScriptBundle("~/bundles/jquerymask").Include(
                    "~/Scripts/jquery/jquery.mask.min.js"))

        bundles.Add(New ScriptBundle("~/bundles/utils").Include(
                    "~/Scripts/toastr/js/toastr.min.js",
                    "~/Scripts/select2/js/select2.min.js"))

        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr/modernizr-*"))

        bundles.Add(New Bundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap/bootstrap.min.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap/bootstrap.min.css",
                  "~/Content/global/site.css",
                  "~/Content/toastr/css/toastr.min.css",
                  "~/Content/tabulator/css/tabulator.min.css",
                  "~/Content/select2/css/select2.min.css",
                  "~/Content/font-awesome-icons/css/font-awesome.min.css"))

        BundleTable.EnableOptimizations = False
    End Sub
End Module

