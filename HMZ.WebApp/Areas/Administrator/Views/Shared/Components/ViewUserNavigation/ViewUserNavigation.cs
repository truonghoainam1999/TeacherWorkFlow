using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Views.Shared.Components.ViewUserNavigation
{
    public class ViewUserNavigation : ViewComponent
    {
        public ViewUserNavigation()
        {
            
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
