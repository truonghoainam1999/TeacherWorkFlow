using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.TaskWork;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class TaskWorkController : BaseController<ITaskWorkService>
    {
        public TaskWorkController(ITaskWorkService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
