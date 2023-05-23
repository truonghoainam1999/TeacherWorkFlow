using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Services.ClassRoomService;
using HMZ.Service.Services.ScheduleService;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class ScheduleController : CRUDBaseControlle<IScheduleService, ScheduleQuery, ScheduleView, ScheduleFilter>
    {
        private readonly IClassRoomService _classRoomService;

        public ScheduleController(IScheduleService service, IClassRoomService classRoomService) : base(service)
        {
            _classRoomService = classRoomService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var classRooms = await _classRoomService.GetAll();
            ViewBag.Rooms = classRooms.Items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScheduleQuery query)
        {
            return await base.Update(query);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            return await base.Delete(id);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(string id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> GetByCode(string code)
        {
            return await base.GetByCode(code);
        }
    }
}
