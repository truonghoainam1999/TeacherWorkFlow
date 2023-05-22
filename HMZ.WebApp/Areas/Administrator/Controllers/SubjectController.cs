using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
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
        public SubjectController(ISubjectService service) : base(service)
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
