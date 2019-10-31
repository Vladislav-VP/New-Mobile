using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using DataAccess.Context;
using Services;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    public class TodoItemsController : Controller
    {
        private readonly TodoListContext _context;
        private readonly TodoItemService _todoItemService;

        public TodoItemsController(TodoListContext context)
        {
            _context = context;
            _todoItemService = new TodoItemService(_context);            
        }
        
        [HttpGet("userId={userId}")]
        public IActionResult GetUsersTodoItems(int userId)
        {
            UsersService usersService = new UsersService(_context);
            User user = usersService.FindById(userId);
            if (user == null)
            {
                return NotFound();
            }
            IEnumerable<TodoItem> todoItems = _todoItemService.GetUsersTodoItems(userId);
            var result = new ObjectResult(todoItems);
            return result;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAllTodoItems()
        {// TODO: Remove this method later?
            IEnumerable<TodoItem> todoItems = null;

            todoItems = _todoItemService.GetAllObjects();

            return todoItems;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TodoItem todoItem = _todoItemService.FindById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            var result = new ObjectResult(todoItem);
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody]TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            _todoItemService.Insert(todoItem);
            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]TodoItem todoItem)
        { // TODO : Refactor this method.
            if (todoItem == null)
            {
                return BadRequest();
            }

            IEnumerable<TodoItem> todoItems = _todoItemService.GetAllObjects();
            bool isFound = todoItems.Any(t => t.Id == todoItem.Id);
            if (!isFound)
            {
                return NotFound();
            }

            TodoItem todoItemToBeModified = _todoItemService.FindById(todoItem.Id);
            todoItemToBeModified.Description = todoItem.Description;

            _todoItemService.Update(todoItem);
            return Ok(todoItem);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TodoItem todoItem = _todoItemService.FindById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _todoItemService.Delete(id);
            return Ok(todoItem);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
