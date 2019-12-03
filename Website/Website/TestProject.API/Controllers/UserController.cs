using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using Services.UI;
using ViewModels.UI.User;
using TestProject.API.Helpers;

namespace TestProject.API.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ImageHelper _imageHelper;

        public UserController(IWebHostEnvironment hostEnvironment)
        {
            _usersService = new UsersService();
            _imageHelper = new ImageHelper();
            _hostEnvironment = hostEnvironment;
        }

        [Route("HomeInfo")]
        [HttpGet]
        public IActionResult HomeInfo(string id)
        {
            HomeInfoUserView user = _usersService.GetUserHomeInfo(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(RequestCreateUserView user)
        {
            ResponseCreateUserView response = _usersService.Register(user);
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
        public IActionResult Settings(string id)
        {
            SettingsUserView user = _usersService.GetUserSettings(id);
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
        public IActionResult ChangePassword(RequestChangePasswordUserView user)
        {
            ResponseChangePasswordUserView response = _usersService.ChangePassword(user);
            return RedirectToAction("Settings", "User", new { user.Id });
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
    }
}