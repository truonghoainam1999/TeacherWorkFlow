using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.RoleServices
{
	public interface IRoleService : IBaseService<RoleQuery, RoleView, RoleFilter>
	{
		
	}
}
