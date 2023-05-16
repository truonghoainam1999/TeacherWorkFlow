using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.PermissionServices
{
    public interface IPermissionService : IBaseService<PermissionQuery, PermissionView, PermissionFilter>
    {
        Task<DataResult<bool>> AddToRolePermissionAsync(Guid roleId, Guid permissionId);
        Task<DataResult<int>> RemoveRolePermissionAsync(Guid roleId, Guid permissionId);
        Task<DataResult<bool>> UpdateRolePermissionAsync(PermissionQuery entity, Guid roleId, Guid permissionId);

        // Get by role id
        Task<DataResult<RoleView>> GetByRoleAsync(Guid roleId);
        Task<DataResult<RoleView>> GetByRoleAsync(string roleName);
        // get by user 
        Task<DataResult<UserView>> GetByUserAsync(Guid userId);
        Task<DataResult<UserView>> GetByUserAsync(string username);

        // get all  role permissions
        Task<DataResult<PermissionView>> GetAllRolePermissionsAsync(BaseQuery<PermissionFilter> query);
    }
}
