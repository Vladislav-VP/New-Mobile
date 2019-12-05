using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Services.Interfaces;
using TestProject.API.Helpers;
using ViewModels.UI.User;

namespace TestProject.API.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ImageHelper _imageHelper;

        public UserController(IWebHostEnvironment hostEnvironment, IUsersService usersService)
        {
            _usersService = usersService;
            _imageHelper = new ImageHelper();
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult HomeInfo()
        {
            HomeInfoUserView user = _usersService.GetUserHomeInfo(User);
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RequestLoginUserView user)
        {
            ResponseLoginUserView response = await _usersService.Login(user);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("Error", response.Message);
                return View("Index");
            }
            return RedirectToAction("HomeInfo", new { user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestCreateUserView user)
        {
            var response = await _usersService.Register(user);
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
        public IActionResult Settings()
        {
            SettingsUserView user = _usersService.GetUserSettings(User);
            return View(user);
        }

        [Route("Settings/{user.Id}")]
        [HttpPost]
        public IActionResult ChangeName(RequestChangeNameUserView user)
        {
            // TODO : Implement logic for showing error messages.
            ResponseChangeNameUserView response = _usersService.ChangeUsername(user);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", response.Message);
                return View("Settings");
            }
            return RedirectToAction("Settings", "User", new { user.Id });            
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RequestChangePasswordUserView user)
        {
            ResponseChangePasswordUserView response = await _usersService.ChangePassword(user, User);
            return RedirectToAction("Settings", "User", user);
        }

        [HttpPost]
        public IActionResult ChangeProfilePhoto(IFormFile file, RequestChangeProfilePhotoUserView user)
        {
            if (file != null)
            {
                user.ImageUrl = $"{_hostEnvironment.WebRootPath}\\ProfileImages\\{Guid.NewGuid()}.png";
                user.ImageBytes = _imageHelper.GetImageBytes(file);
                ResponseChangeProfilePhotoUserView response = _usersService.ChangeProfilePhoto(user);
            }            
            return RedirectToAction("Settings", "User", new { user.Id });
        }

        [HttpPost]
        public IActionResult RemoveProfilePhoto(string id)
        {
            _usersService.RemoveProfilePhoto(id);
            return RedirectToAction("Settings", "User", new { id });
        }

        [HttpPost]
        public IActionResult DeleteAccount(string id)
        {
            _usersService.DeleteAccount(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usersService.Logout();
            return RedirectToAction("Login", "User");
        }
    }
}