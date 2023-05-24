using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Services.ClassRoomService;
using HMZ.Service.Services.SubjectServices;
using HMZ.Service.Services.TaskWorkServices;
using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class TaskWorkController : BaseController<ITaskWorkService>
    {
        private readonly IClassRoomService _classRoomService;
        private readonly IUserService _userService;
        private readonly ISubjectService _subjectService;

        public TaskWorkController(ITaskWorkService service, IClassRoomService classRoomService, IUserService userService, ISubjectService subjectService) : base(service)
        {
            _classRoomService = classRoomService;
            _userService = userService;
            _subjectService = subjectService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var classRooms = await _classRoomService.GetAll();
            var users = await _userService.GetAll();
            var subject = await _subjectService.GetAll();

            ViewBag.Rooms = classRooms.Items;
            ViewBag.Users = users.Items;
            ViewBag.Subjects = subject.Items;
            return View();
        }
        public async Task<IActionResult> Update()
        {
            var classRooms = await _classRoomService.GetAll();
            var users = await _userService.GetAll();
            var subject = await _subjectService.GetAll();

            ViewBag.Rooms = classRooms.Items;
            ViewBag.Users = users.Items;
            ViewBag.Subjects = subject.Items;
            return View();
        }

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery<TaskWorkFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var TaskWorks = await _service.GetPageList(query);
            return Ok(TaskWorks);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskWorkQuery query)
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
        public async Task<IActionResult> Update([FromBody] TaskWorkQuery query, string id)
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
