using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.DepartmentServices
{
	public interface IDepartmentService : IBaseService<DepartmentQuery, DepartmentView, DepartmentFilter>
	{
		Task<DataResult<DepartmentView>> GetAll();
	}
}
