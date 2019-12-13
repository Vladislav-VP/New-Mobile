using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using ViewModels.Api.User;
using Services.Interfaces;

namespace TestProject.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserApiController : Controller
    {
        private readonly IUsersApiService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserApiController(IWebHostEnvironment hostEnvironment, IUsersApiService usersService)
        {
            _hostEnvironment = hostEnvironment;
            _usersService = usersService;
        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseRegisterUserApiView> Register([FromBody] RequestRegisterUserApiView user)
        {
            ResponseRegisterUserApiView response = await _usersService.Register(user);
            return response;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseLoginUserApiView> Login([FromBody] RequestLoginUserApiView user)
        {
            ResponseLoginUserApiView userForLogin = await _usersService.Login(user, User);
            return userForLogin;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<DeleteUserApiView> Delete()
        {
            DeleteUserApiView response = await _usersService.Delete(User);
            return response;
        }

        [Route("EditProfileImage")]
        [HttpPost]
        public async Task<ResponseEditProfileImageUserApiView> EditProfileImage([FromBody] RequestEditProfileImageUserApiView user)
        {
            string imageUrl = $"{_hostEnvironment.WebRootPath}\\ProfileImages\\{Guid.NewGuid()}.png";
            ResponseEditProfileImageUserApiView response = await _usersService.ReplaceProfilePhoto(user, imageUrl, User);
            return response;            
        }

        [Route("GetProfileImage")]
        [HttpGet]
        public async Task<GetProfileImageUserApiView> GetProfileImage()
        {
            var user = await _usersService.GetUserWithPhoto(User);
            return user;
        }

        [Route("GetUserName")]
        [HttpGet]
        public async Task<string> GetUserName()
        {
            string name = await _usersService.GetUserName(User);
            return name;
        }

        [Route("EditName")]
        [HttpPost]
        public async Task<ResponseEditNameUserApiView> EditName(RequestEditNameUserApiView user)
        {
            ResponseEditNameUserApiView response = await _usersService.EditUserName(user, User);
            return response;
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ResponseChangePasswordUserApiView> ChangePassword(RequestChangePasswordUserApiView user)
        {
            ResponseChangePasswordUserApiView response = await _usersService.ChangePassword(user, User);
            return response;
        }

        [Route("Logout")]
        [HttpPost]
        public async Task Logout()
        {
            await _usersService.Logout(User);
        }

        [Route("RefreshToken")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseRefreshAccessTokenUserApiView> RefreshToken([FromBody] RequestRefreshAccessTokenUserApiView requestRefresh)
        {
            ResponseRefreshAccessTokenUserApiView response = await _usersService.RefreshToken(requestRefresh);
            return response;
        }

        [Route("ConfirmEmail")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(string token)
        {
            // TODO : Implement normal verification
            return Ok("Verified");
        }
    }
}