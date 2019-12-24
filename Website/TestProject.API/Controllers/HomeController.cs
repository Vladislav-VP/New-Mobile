using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        public IActionResult EmailConfirmed()
        {
            return Ok("Email confirmed!");
        }
    }
}