using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.Room;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class RoomController : BaseController<IRoomService>
    {
        public RoomController(IRoomService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
