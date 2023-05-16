using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class RolesController : BaseController<IUserService>
    {
        public RolesController(IUserService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
