using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Views.Shared.Components.ViewBreadcrumb
{
    public class ViewBreadcrumb : ViewComponent
    {

        public ViewBreadcrumb()
        {

        }

        public IViewComponentResult Invoke()
        {
            var breadcrumb = GetBreadcrumb();
            ViewBag.Breadcrumb = breadcrumb;
            return View();
        }
        private string[] GetBreadcrumb()
        {
            var routeData = HttpContext.Request.RouteValues;
            var controller = routeData["controller"] ?? "";
            var action = routeData["action"] ?? "";
            var area = routeData["area"] ?? "";
            var id = routeData["id"] ?? "";
            var breadcrumb = new string[4];
            breadcrumb[0] = area.ToString();
            breadcrumb[1] = controller.ToString();
            breadcrumb[2] = action.ToString();
            breadcrumb[3] = id.ToString();
            return breadcrumb;
        }

    }
}
