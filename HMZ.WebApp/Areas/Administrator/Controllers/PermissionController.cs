using HMZ.Service.Services.DashboardServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class PermissionController : BaseController<IDashboardService>
    {
        public PermissionController(IDashboardService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
