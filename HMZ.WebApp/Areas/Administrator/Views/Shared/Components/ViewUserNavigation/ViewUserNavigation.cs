using HMZ.DTOs.Views;
using HMZ.Service.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Views.Shared.Components.ViewUserNavigation
{
    public class ViewUserNavigation : ViewComponent
    {
        UserView _user;
        private readonly IUserService _userService;
        public ViewUserNavigation( IUserService userService)
        {
            _userService = userService;
        }
        public IViewComponentResult Invoke()
        {
            _user = LoadUserNavigation().Result;
            return View(_user);
        }

        private async Task<UserView> LoadUserNavigation()
        {
            string username = HttpContext.User.Identity.Name;

            var user = await _userService.GetByUserName(username);
            return user.Entity;
        }

        
    }
}
