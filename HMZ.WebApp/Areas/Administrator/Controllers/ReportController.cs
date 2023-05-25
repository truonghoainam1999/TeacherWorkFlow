using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.Service.Services.Reports;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
	public class ReportController : BaseController<IReportService>
	{

		public ReportController(IReportService service) : base(service)
		{
		}
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Delay()
        {
            return View();
        }
        public IActionResult DeadLine()
        {
            return View();
        }
        // All task
        [HttpPost]
		public async Task<IActionResult> GetAll(BaseQuery<ReportFilter> query)
		{
			query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
			query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
      
			var Reports = await _service.GetAllPageList(query);
			return Ok(Reports);
		}
        // Đến hạn
        [HttpPost]
        public async Task<IActionResult> GetDelay(BaseQuery<ReportFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;

            var Reports = await _service.GetDeLayPageList(query);
            return Ok(Reports);
        }
        // Hết hạn
        [HttpPost]
        public async Task<IActionResult> GetDeadLine(BaseQuery<ReportFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;

            var Reports = await _service.GetDeadLinePageList(query);
            return Ok(Reports);
        }
    }
}
