using Microsoft.AspNetCore.Mvc;

using Services.UI;
using ViewModels.UI.Home;

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
            return View();
        }

        [HttpPost]
        public IActionResult Login(RequestLoginHomeView user)
        {
            ResponseLoginHomeView response = _usersService.Login(user);
            if (!response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("HomeInfo", "User", new { user.Id });
        }
        
        public IActionResult Register()
        {
            return View();
        }
    }
}