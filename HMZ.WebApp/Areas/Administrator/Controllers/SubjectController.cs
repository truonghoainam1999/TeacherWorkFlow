using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
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
    public class SubjectController : CRUDBaseControlle<ISubjectService,SubjectQuery,SubjectView,SubjectFilter>
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
	}
}
