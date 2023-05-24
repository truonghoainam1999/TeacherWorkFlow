using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Services.ClassRoomService;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class ClassRoomController : CRUDBaseControlle<IClassRoomService, ClassRoomQuery, ClassRoomView, ClassRoomFilter>
    {
        public ClassRoomController(IClassRoomService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update()
        {

            return View();
        }
    }
}
