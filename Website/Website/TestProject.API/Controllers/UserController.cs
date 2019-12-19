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

        // TODO : Implement logic for showing error messages.

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
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RequestLoginUserView user)
        {
            ResponseLoginUserView response = await _usersService.Login(user, User);
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

        [HttpPost]
        public async Task<IActionResult> ChangeName(RequestChangeNameUserView user)
        {
            ResponseChangeNameUserView response = await _usersService.ChangeUsername(user, User);
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
                ResponseChangeProfilePhotoUserView response = _usersService.ChangeProfilePhoto(user, User);
            }            
            return RedirectToAction("Settings", "User");
        }

        [HttpPost]
        public IActionResult RemoveProfilePhoto()
        {
            _usersService.RemoveProfilePhoto(User);
            return RedirectToAction("Settings", "User");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            await _usersService.DeleteAccount(User);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _usersService.Logout(User);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email)
        {
            var requestPassword = new RequestResetPasswordUserView
            {
                Email = email
            };
            return View(requestPassword);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(RequestResetPasswordUserView requestReset)
        {
            ResponseResetPasswordUserView response = await _usersService.ResetPassword(requestReset);
            if (!response.IsSuccess)
            {
                return View(requestReset);
            }
            return RedirectToAction("Login");
        }
    }
}