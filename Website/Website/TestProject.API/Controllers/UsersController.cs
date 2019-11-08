using DataAccess.Context;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using TestProject.API.Helpers;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TodoListContext _context;
        private readonly UsersService _usersService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserEditHelper _userEditHelper;

        public UsersController(TodoListContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _usersService = new UsersService(_context);
            _hostingEnvironment = hostingEnvironment;
            _userEditHelper = new UserEditHelper(_usersService);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            IEnumerable<User> users = _usersService.GetAllObjects();            
            if (users == null)
            {
                return NotFound();
            }
            var result = new ObjectResult(users);
            return result;
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
        public IActionResult GetUser(string username)
        {
            User user = _usersService.Find(username);
            if (user == null)
            {
                return NotFound();
            }
            var result = new ObjectResult(user);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody]  User user)
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
        }

        [HttpPost]
        public IActionResult Register([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                ModelState.AddModelError(nameof(user.Name), "Username can not be empty");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError(nameof(user.Password), "Password can not be empty");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _usersService.Register(user.Name, user.Password);
            return Ok(user);
        }

        [HttpPost("username={username}/password={password}")]
        public IActionResult Login(string username, string password)
        {
            User retrievedUser = _usersService.Find(username, password);
            if (retrievedUser == null)
            {
                return NotFound();
            }
            return Ok(retrievedUser);
        }

        [HttpDelete("{id}")]
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

        [HttpPost("EditProfileImage")]
        public IActionResult EditProfileImage([FromBody] User user)
        {
            _userEditHelper.ReplaceProfilePhoto(_hostingEnvironment, user);
            return Ok();            
        }

        [HttpGet("GetProfileImage/{id}")]
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

        [HttpPut("RemoveProfileImage/{id}")]
        public IActionResult RemoveProfileImage(int id)
        {
            User user = _usersService.FindById(id);
            return Ok();
        }
    }
}