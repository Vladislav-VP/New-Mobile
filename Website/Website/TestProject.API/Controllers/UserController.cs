using Microsoft.AspNetCore.Mvc;

using Services.UI;
using ViewModels.UI;

namespace TestProject.API.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersService _usersService;

        public UserController()
        {
            _usersService = new UsersService();
        }

        [Route("HomeInfo")]
        [HttpGet]
        public IActionResult HomeInfo(int id)
        {
            HomeInfoUserView user = _usersService.GetUserHomeInfo(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult CreateNewUser(RequestRegisterUserView user)
        {
            ResponseRegisterUserView response = _usersService.Register(user);
            if (!response.IsSuccess)
            {
                return RedirectToAction("Register", "Home");
            }            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult BackToLogin()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Settings(int id)
        {
            SettingsUserView user = _usersService.GetUserSettings(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeName(RequestChangeNameUserView user)
        {
            // TODO : Implement logic for showing error messages.
            ResponseChangeNameUserView response = _usersService.ChangeUsername(user);
            bool valid = ModelState.IsValid;
            if (!ModelState.IsValid)
            {
                return View("Settings", user);
            }
            return RedirectToAction("Settings", "User", new { user.Id });
            
        }

        [HttpPost]
        public IActionResult ChangePassword(RequestChangePasswordUserView user)
        {
            ResponseChangePasswordUserView response = _usersService.ChangePassword(user);
            return RedirectToAction("Settings", "User", new { user.Id });
        }
    }
}