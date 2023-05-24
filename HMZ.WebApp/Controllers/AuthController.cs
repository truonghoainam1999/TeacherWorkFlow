using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Services.UserServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMZ.WebApp.Controllers
{
    public class AuthController : BaseController<IUserService>
    {
        public AuthController(IUserService service) : base(service)
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _service.Login(query);
            if (result.Success == false)
            {
                return Ok(result);
            }
            //Login MVC
            var user = result.Entity as UserView;
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { identity });
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties{ IsPersistent = true });
            return Ok(result);
        }
    }
}