using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.Service.Services.DepartmentServices;
using HMZ.Service.Services.SubjectServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    // T: Service
    // T1: Query
    // T2: View
    // T3: Filter
    public class SubjectController : BaseController<ISubjectService>
    {
        private readonly IDepartmentService _departmentService;

        public SubjectController(ISubjectService service, IDepartmentService departmentService) : base(service)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = departments.Items;
            return View();
        }
        public async Task<IActionResult> Update()
        {
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = departments.Items;
            return View();
        }

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<SubjectFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var subjects = await _service.GetPageList(query);
            return Ok(subjects);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectQuery query)
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
        public async Task<IActionResult> Update([FromBody] SubjectQuery query, string id)
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
