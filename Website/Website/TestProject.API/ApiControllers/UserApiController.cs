using DataAccess.Context;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using ViewModels.Api;
using ViewModels.Api.User;

namespace TestProject.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : Controller
    {
        private readonly TodoListContext _context;
        private readonly UsersService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserApiController(TodoListContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _usersService = new UsersService(_context);
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = _usersService.GetAllObjects();
            return users;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _usersService.FindById(id);
            if (user == null)
            {
                ModelState.AddModelError(nameof(user), "Incorrect login or password");
                return NotFound(ModelState);
            }
            var result = new ObjectResult(user);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult EditData([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            User userToModify = _usersService.FindById(user.Id);
            if (userToModify == null)
            {
                return NotFound();
            }
            _usersService.EditUser(user);
            return Ok(user);
            //ResponseEditDataUserApiView response = _usersService.EditUser(user);
            //if (!response.IsSuccess)
            //{
            //    response = null;
            //}
            //return response;
            //throw new NotImplementedException();
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

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            User user = _usersService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            _usersService.Delete(id);
            return Ok();
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
    }
}