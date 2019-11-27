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
    }
}