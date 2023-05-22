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
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ClassRoomQuery query)
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
