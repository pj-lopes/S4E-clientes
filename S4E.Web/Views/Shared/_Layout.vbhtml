<!DOCTYPE html>
<html lang="@ViewBag.Culture">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - S4E</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("stylesheet", required:=False)
</head>
<body class="bg-cinza-body">
    @RenderSection("modal", required:=False)
    @RenderBody()


    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/jquerymask")
    @Scripts.Render("~/bundles/bootstrap")
    <script src="~/Scripts/tabulator/js/tabulator.min.js"></script>
    @Scripts.Render("~/bundles/utils")
    @RenderSection("scripts", required:=False)
</body>
</html>
