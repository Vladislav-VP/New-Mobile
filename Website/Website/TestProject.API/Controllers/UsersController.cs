using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TodoListContext _context;
        private readonly UsersService _usersService;

        public UsersController(TodoListContext context)        
        {
            _context = context;
            _usersService = new UsersService(_context);
            if (_context.Users.Count() == 0)
            {
                AddMockedUser();
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var user = new User
            {
                Name = string.Empty,
                Password = string.Empty
            };
            Register(user);
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

        // TODO: Remove this method later.
        private void AddMockedUser()
        {
            var user = new User
            {
                Name = "vp",
                Password = "qwerty"
            };
            _usersService.Register(user.Name, user.Password);
        }
    }
}