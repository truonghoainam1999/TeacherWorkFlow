using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.Subject;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class SubjectController : BaseController<ISubjectService>
    {
        public SubjectController(ISubjectService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
