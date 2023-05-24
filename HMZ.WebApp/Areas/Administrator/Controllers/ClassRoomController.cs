using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Services.ClassRoomService;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class ClassRoomController : BaseController<IClassRoomService>
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

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery<ClassRoomFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var ClassRooms = await _service.GetPageList(query);
            return Ok(ClassRooms);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassRoomQuery query)
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
        public async Task<IActionResult> Update([FromBody] ClassRoomQuery query, string id)
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

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<ClassRoomFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var classRooms = await _service.GetPageList(query);
            return Ok(classRooms);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassRoomQuery query)
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
        public async Task<IActionResult> Update([FromBody] ClassRoomQuery query, string id)
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