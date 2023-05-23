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
            var users = await _userService.GetAll();
            var subject = await _subjectService.GetAll();

            ViewBag.Rooms = classRooms.Items;
            ViewBag.Users = users.Items;
            ViewBag.Subjects = subject.Items;
            return View();
        }
    }
}
