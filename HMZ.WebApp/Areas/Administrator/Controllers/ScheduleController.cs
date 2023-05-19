using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.Schedule;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class ScheduleController : BaseController<IScheduleService>
    {
        public ScheduleController(IScheduleService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
