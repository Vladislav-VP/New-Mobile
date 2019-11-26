using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Repositories;
using DataAccess.Context;
using Services;
using ViewModels.Api.TodoItem;

namespace TestProject.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemApiController : Controller
    {
        private readonly TodoListContext _context;
        private readonly TodoItemService _todoItemService;

        public TodoItemApiController(TodoListContext context)
        {
            _context = context;
            _todoItemService = new TodoItemService(_context);            
        }
        
        [Route("GetUsersTodoItems/userId={userId}")]
        [HttpGet]
        public GetListTodoItemApiView GetList(int userId)
        {
            GetListTodoItemApiView usersTodoItems = _todoItemService.GetUsersTodoItems(userId);
            return usersTodoItems;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public GetTodoItemApiView Get(int id)
        {
            GetTodoItemApiView todoItem = _todoItemService.GetTodoItem(id);
            return todoItem;
        }

        [Route("Create")]
        [HttpPost]
        public ResponseCreateTodoItemApiView Create([FromBody]RequestCreateTodoItemApiView todoItem)
        {
            ResponseCreateTodoItemApiView response = _todoItemService.Insert(todoItem);
            return response;
        }

        [Route("Edit")]
        [HttpPost]
        public ResponseEditTodoItemApiView Edit([FromBody]RequestEditTodoItemApiView todoItem)
        {
            ResponseEditTodoItemApiView response = _todoItemService.EditTodoItem(todoItem);
            return response;
        }
        
        [Route("Delete/{id}")]
        [HttpDelete]
        public DeleteTodoItemApiView Delete(int id)
        {
            DeleteTodoItemApiView response = _todoItemService.Delete(id);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
