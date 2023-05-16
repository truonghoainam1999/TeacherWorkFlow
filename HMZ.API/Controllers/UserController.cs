using HMZ.API.Controllers.Base;
using HMZ.Database.Entities;
using HMZ.Database.Enums;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.SDK.Excel;
using HMZ.Service.Enums;
using HMZ.Service.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.API.Controllers
{

    //[Authorize(Policy = "Admin")]
    public class UserController : BaseController<IUserService>
    {
        public UserController(IUserService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<IActionResult> GetAll(BaseQuery<UserFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;

            var users = await _service.GetPageList(query);

            return users.Items == null ? BadRequest(users) : Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user.Entity == null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserQuery user)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var result = await _service.UpdateAsync(user, id);
            if (result.Entity != 1)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserQuery user)
        {
            if (user == null)
            {
                return NotFound();
            }
            var result = await _service.CreateAsync(user);
            if (result.Entity != true)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var result = await _service.DeleteAsync(id);
            if (result.Entity != 1)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel(BaseQuery<UserFilter> query)
        {
            query.PageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 10;
            var result = await _service.GetPageList(query);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            var exportExcel = new ExcelExportBase<UserView>();
            var exportResult = await exportExcel.ExportToExcel(result.Items, "Export_User.xlsx");
            if (exportResult.ErrorMessage != null)
            {
                return BadRequest(exportResult.ErrorMessage);
            }
            return new FileContentResult(exportResult.Content, exportResult.ContentType)
            {
                FileDownloadName = exportResult.FileName
            };

        }

        // Forgot password
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            string host = $"{Request.Scheme}://{Request.Host}";
            var result = await _service.ForgotPassword(email, host);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        // Reset password
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromQuery] string email, [FromBody] UpdatePasswordQuery? entity)
        {
            entity.Token = token;
            entity.Email = email;
            var result = await _service.ResetPassword(entity);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // lock user
        [HttpPost]
        public async Task<IActionResult> LockUser(string username, bool isLock)
        {
            var result = await _service.LockUser(username, isLock);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
