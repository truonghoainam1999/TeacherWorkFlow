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
            breadcrumb[1] = GetTranslatedControllerName(controller.ToString());
            breadcrumb[2] = action.ToString();
            breadcrumb[3] = id.ToString();
            return breadcrumb;
        }
		private string GetTranslatedControllerName(string controllerName)
		{
			switch (controllerName.ToLower())
			{
                case "classRoom":
                    return "Phòng học";
				case "department":
					return "Khoa";
				case "report":
					return "Báo cáo";
				case "roles":
					return "Phân quyền";
				case "schedule":
					return "Thời khóa biểu";
				case "subject":
					return "Bộ môn";
				case "taskWork":
					return "Lịch làm việc";
				case "users":
					return "Người dùng";
				default:
					return controllerName;
			}
		}
	}
}
