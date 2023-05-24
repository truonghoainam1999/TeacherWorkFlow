using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Services.ClassRoomService;
using HMZ.Service.Services.ScheduleService;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class ScheduleController : BaseController<IScheduleService>
    {
        private readonly IClassRoomService _classRoomService;

        public ScheduleController(IScheduleService service, IClassRoomService  classRoomService) : base(service)
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
            ViewBag.ClassRooms = classRooms.Items;
            return View();
        }
        public async Task<IActionResult> Update()
        {
            var classRooms = await _classRoomService.GetAll();
            ViewBag.ClassRooms = classRooms.Items;
            return View();
        }

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery<ScheduleFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var Schedules = await _service.GetPageList(query);
            return Ok(Schedules);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScheduleQuery query)
        {

            var result = await _service.CreateAsync(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScheduleQuery query, string id)
        {
            var result = await _service.UpdateAsync(query, id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        #endregion
    }
}
