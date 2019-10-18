using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.API.Context;
using TestProject.API.Repositories;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TodoListContext _context;
        private readonly UserRepository _userRepository;

        public UsersController(TodoListContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }
    }
}