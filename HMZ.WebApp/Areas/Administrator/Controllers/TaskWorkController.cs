using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Services.ClassRoomService;
using HMZ.Service.Services.SubjectServices;
using HMZ.Service.Services.TaskWorkServices;
using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class TaskWorkController : CRUDBaseControlle<ITaskWorkService, TaskWorkQuery, TaskWorkView, TaskWorkFilter>
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
           // var users = await _userService.GetAll();
           // var subject = await _subjectService.GetAll();
            ViewBag.Rooms = classRooms.Items;
           // ViewBag.Users = users.Items;
           // ViewBag.Subjects = subject.Items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TaskWorkQuery query)
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
