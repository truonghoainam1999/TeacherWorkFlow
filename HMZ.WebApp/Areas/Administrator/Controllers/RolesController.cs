using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.Service.Services.RoleServices;
using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class RolesController : BaseController<IRoleService>
    {
        public RolesController(IRoleService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<RoleFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var users = await _service.GetPageList(query);
            return users.Items == null ? BadRequest(users) : Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleQuery query)
        {
            var result = await _service.CreateAsync(query);
            return result.Entity ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return result.Entity > 0 ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleQuery query)
        {
            var result = await _service.UpdateAsync(query, query.Name);
            return result.Entity >0 ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetById(string roleName)
        {
            var result = await _service.GetByIdAsync(roleName);
            return result.Entity == null ?  BadRequest(result) : Ok(result);
        }

    }
}
