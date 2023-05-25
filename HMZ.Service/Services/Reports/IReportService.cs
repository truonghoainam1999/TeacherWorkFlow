using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Services.Reports
{
	public interface IReportService
	{
        Task<DataResult<ReportView>> GetDeadLinePageList(BaseQuery<ReportFilter> query);
        Task<DataResult<ReportView>> GetDeLayPageList(BaseQuery<ReportFilter> query);
        Task<DataResult<ReportView>> GetAllPageList(BaseQuery<ReportFilter> query);

    }
}
