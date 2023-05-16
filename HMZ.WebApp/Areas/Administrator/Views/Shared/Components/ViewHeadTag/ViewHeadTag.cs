using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Views.Shared.Components.ViewHeadTag
{
	public class ViewHeadTag: ViewComponent
	{

        public ViewHeadTag()
        {
            
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
