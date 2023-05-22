using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.DepartmentServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class DepartmentController : BaseController<IDepartmentService>
    {
        public DepartmentController(IDepartmentService service) : base(service)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
