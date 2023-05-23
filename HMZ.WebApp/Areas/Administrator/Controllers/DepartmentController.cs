using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Services.DepartmentServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class DepartmentController : CRUDBaseControlle<IDepartmentService, DepartmentQuery, DepartmentView, DepartmentFilter>
    {
        public DepartmentController(IDepartmentService service) : base(service)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
