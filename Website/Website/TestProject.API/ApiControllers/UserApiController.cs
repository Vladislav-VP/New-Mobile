using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

using ViewModels.Api.User;
using Services.Api;

namespace TestProject.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : Controller
    {
        private readonly UsersApiService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserApiController(IWebHostEnvironment hostEnvironment)
        {
            _usersService = new UsersApiService();
            _hostEnvironment = hostEnvironment;
        }

        [Route("Register")]
        [HttpPost]
        public ResponseRegisterUserApiView Register([FromBody] RequestRegisterUserApiView user)
        {
            ResponseRegisterUserApiView response = _usersService.Register(user);
            return response;
        }

        [Route("username={username}/password={password}")]
        [HttpPost]
        public ResponseLoginUserApiView Login(RequestLoginUserApiView user)
        {
            ResponseLoginUserApiView userForLogin = _usersService.Login(user);
            return userForLogin;
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public DeleteUserApiView Delete(int id)
        {
            DeleteUserApiView response = _usersService.Delete(id);
            return response;
        }

        [Route("EditProfileImage")]
        [HttpPost]
        public ResponseEditProfileImageUserApiView EditProfileImage([FromBody] RequestEditProfileImageUserApiView user)
        {
            string imageUrl = $"{_hostEnvironment.WebRootPath}\\ProfileImages\\{Guid.NewGuid()}.png";
            ResponseEditProfileImageUserApiView response = _usersService.ReplaceProfilePhoto(user, imageUrl);
            return response;            
        }

        [Route("GetProfileImage/{id}")]
        [HttpGet]
        public GetProfileImageUserApiView GetProfileImage(int id)
        {
            var user = _usersService.GetUserWithPhoto(id);
            return user;
        }

        [Route("GetUserName/{id}")]
        [HttpGet]
        public string GetUserName(int id)
        {
            string name = _usersService.GetUserName(id);
            return name;
        }

        [Route("EditName")]
        [HttpPost]
        public ResponseEditNameUserApiView EditName(RequestEditNameUserApiView user)
        {
            ResponseEditNameUserApiView response = _usersService.EditUserName(user);
            return response;
        }

        [Route("ChangePassword")]
        [HttpPost]
        public ResponseChangePasswordUserApiView ChangePassword(RequestChangePasswordUserApiView user)
        {
            ResponseChangePasswordUserApiView response = _usersService.ChangePassword(user);
            return response;
        }
    }
}