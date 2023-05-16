using HMZ.API.Controllers.Base;
using HMZ.DTOs.Queries;
using HMZ.Service.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.API.Controllers
{
    public class AuthController : BaseController<IUserService>
    {
        public AuthController(IUserService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginQuery user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(user);
                if (result.Entity == null)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            return GetResponseError(ModelState.Values.First().Errors.First().ErrorMessage);

        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterQuery user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Register(user);
                if (result == null)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState.Values.First().Errors.First().ErrorMessage);
        }
    }
}
