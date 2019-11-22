using DataAccess.Context;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using TestProject.API.Helpers;
using ViewModels.Api;

namespace TestProject.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : Controller
    {
        private readonly TodoListContext _context;
        private readonly UsersService _usersService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserEditHelper _userEditHelper;

        public UserApiController(TodoListContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _usersService = new UsersService(_context);
            _hostEnvironment = hostingEnvironment;
            _userEditHelper = new UserEditHelper(_usersService);
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

        [HttpGet("username={username}")]
        public GetByNameUserApiView GetByName(string username)
        {
            // TODO: Refactor this method
            GetByNameUserApiView user = _usersService.Find(username);
            return user;
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
        public IActionResult EditProfileImage([FromBody] User user)
        {
            _userEditHelper.ReplaceProfilePhoto(_hostEnvironment, user);
            return Ok();            
        }

        [Route("GetProfileImage/{id}")]
        [HttpGet]
        public IActionResult GetProfileImage(int id)
        {
            User user = _usersService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(user.ImageUrl))
            {
                user = _userEditHelper.GetUserWithPhoto(id);
            }            

            var result = new ObjectResult(user);
            return result;
        }
    }
}