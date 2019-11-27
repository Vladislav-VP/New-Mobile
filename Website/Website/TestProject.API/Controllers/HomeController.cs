using Microsoft.AspNetCore.Mvc;

using Services.UI;
using ViewModels.UI;

namespace TestProject.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersService _usersService;

        public HomeController()
        {
            _usersService = new UsersService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = new RequestLoginUserView();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(RequestLoginUserView user)
        {
            ResponseLoginUserView response = _usersService.Login(user);
            if (!response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("HomeInfo", "User", new { user.Id });
        }
    }
}