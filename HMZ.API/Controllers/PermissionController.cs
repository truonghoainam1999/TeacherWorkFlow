using HMZ.API.Controllers.Base;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.Service.Services.PermissionServices;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.API.Controllers
{
    public class PermissionController : BaseController<IPermissionService>
    {
        public PermissionController(IPermissionService service) : base(service)
        {
        }

        #region  Permission CRUD
        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<PermissionFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var users = await _service.GetPageList(query);
            return users?.Items == null ? BadRequest(users) : Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _service.GetByIdAsync(id);
            return user.Entity == null ? BadRequest(user) : Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PermissionQuery permissionQuery)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var result = await _service.UpdateAsync(permissionQuery, id);
            return result.Entity == 0 ? BadRequest(result) : Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PermissionQuery permissionQuery)
        {
            if (permissionQuery == null)
            {
                return NotFound();
            }
            var result = await _service.CreateAsync(permissionQuery);
            return result.Entity == false ? BadRequest(result) : Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var result = await _service.DeleteAsync(id);
            return result.Entity == 0 ? BadRequest(result) : Ok(result);
        }
        #endregion

        #region  Permission Role CRUD

        [HttpPost]
        public async Task<IActionResult> GetAllRolePermissions(BaseQuery<PermissionFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var users = await _service.GetAllRolePermissionsAsync(query);
            return users?.Items == null ? BadRequest(users) : Ok(users);
        }
        // get all permissions by user id
        [HttpPost("{id}")]
        public async Task<IActionResult> GetPermissionsByUserId(string id)
        {
            var result = await _service.GetByUserAsync(id);
            return result.Items == null ? BadRequest(result) : Ok(result);
        }
        [HttpPost("{username}")]
        public async Task<IActionResult> GetPermissionsByUsername(string username)
        {
            var result = await _service.GetByUserAsync(username);
            return result.Entity == null ? BadRequest(result) : Ok(result);
        }
        // get all permissions by role id
        [HttpPost("{id}")]
        public async Task<IActionResult> GetPermissionsByRoleId(string id)
        {
            var result = await _service.GetByRoleAsync(id);
            return result.Items == null ? BadRequest(result) : Ok(result);
        }
        [HttpPost("{roleName}")]
        public async Task<IActionResult> GetPermissionsByRoleName(string roleName)
        {
            var result = await _service.GetByRoleAsync(roleName);
            return result.Items == null ? BadRequest(result) : Ok(result);
        }

        [HttpPost("{roleId}/{permissionId}")]
        public async Task<IActionResult> AddToRolePermission(string roleId, string permissionId)
        {
            var result = await _service.AddToRolePermissionAsync(Guid.Parse(roleId), Guid.Parse(permissionId));
            return result.Entity == false ? BadRequest(result) : Ok(result);
        }
        [HttpDelete("{roleId}/{permissionId}")]
        public async Task<IActionResult> RemoveRolePermission(string roleId, string permissionId)
        {
            var result = await _service.RemoveRolePermissionAsync(Guid.Parse(roleId), Guid.Parse(permissionId));
            return result.Entity == 0 ? BadRequest(result) : Ok(result);
        }
        [HttpPut("{roleId}/{permissionId}")]
        public async Task<IActionResult> UpdateRolePermission(string roleId, string permissionId, PermissionQuery permissionQuery)
        {
            var result = await _service.UpdateRolePermissionAsync(permissionQuery, Guid.Parse(roleId), Guid.Parse(permissionId));
            return result.Entity == false ? BadRequest(result) : Ok(result);
        }
        #endregion




    }
}