using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestProject.API.Context;
using TestProject.API.Entities;
using TestProject.API.Repositories;
using TestProject.API.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        private readonly TodoListContext _context;
        private readonly TodoItemRepository _todoItemRepository;

        public TodoListController(TodoListContext context)
        {
            _context = context;
            _todoItemRepository = new TodoItemRepository(_context);

            if (_context.TodoItems.Count() == 0)
            {
                InsertMockedTodoItems();
            }
            
        }
        
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            IEnumerable<TodoItem> todoItems = null;

            todoItems = _todoItemRepository.GetAllObjects();

            return todoItems;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TodoItem todoItem = _todoItemRepository.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(todoItem);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            _todoItemRepository.Insert(todoItem);
            return Ok(todoItem);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            IEnumerable<TodoItem> todoItems = _todoItemRepository.GetAllObjects();
            bool isFound = todoItems.Any(t => t.Id == todoItem.Id);
            if (!isFound)
            {
                return NotFound();
            }

            _todoItemRepository.Update(todoItem);
            return Ok(todoItem);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TodoItem todoItem = _todoItemRepository.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _todoItemRepository.Delete(id);
            return Ok(todoItem);
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        // TODO : Remove this method later.
        private void InsertMockedTodoItems()
        {
            var todoItem1 = new TodoItem { Name = "First", Description = "111" };
            var todoItem2 = new TodoItem { Name = "Second", Description = "222" };

            _todoItemRepository.Insert(todoItem1);
            _todoItemRepository.Insert(todoItem2);
        }        
    }
}
