using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class UsersController : BaseController<IUserService>
    {
        public UsersController(IUserService service) : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<UserFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var users = await _service.GetPageList(query);
            return users.Items == null ? BadRequest(users) : Ok(users);
        }

        // Logout
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth", new { area = "" });
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }

        #region  CRUD
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserQuery query)
        {
            var result = await _service.CreateAsync(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UserQuery query, string id)
        {
            var result = await _service.UpdateAsync(query, id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        #endregion
    }
}
