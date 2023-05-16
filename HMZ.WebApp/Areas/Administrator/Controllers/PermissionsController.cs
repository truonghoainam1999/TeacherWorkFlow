using HMZ.Service.Services.PermissionServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class PermissionsController : BaseController<IPermissionService>
    {
        public PermissionsController(IPermissionService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
