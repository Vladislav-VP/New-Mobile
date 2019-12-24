using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

using DataAccess.Repositories.Interfaces;
using Entities;
using Services.Interfaces;
using ViewModels.Api.TodoItem;

namespace Services.Api
{
    public class TodoItemApiService : BaseApiService<TodoItem>, ITodoItemApiService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly UserManager<User> _userManager;

        public TodoItemApiService(ITodoItemRepository todoItemRepository, UserManager<User> userManager)
            : base()
        {
            _todoItemRepository = todoItemRepository;
            _userManager = userManager;
        }

        public ResponseEditTodoItemApiView EditTodoItem(RequestEditTodoItemApiView todoItem)
        {
            var response = new ResponseEditTodoItemApiView();
            if (string.IsNullOrEmpty(todoItem.Description))
            {
                response.Message = "Todo item description can not be empty";
                return response;
            }
            TodoItem todoItemToModify = _todoItemRepository.FindById(todoItem.Id);
            todoItemToModify.Description = todoItem.Description;
            todoItemToModify.IsDone = todoItem.IsDone;
            _todoItemRepository.Update(todoItemToModify);
            response.IsSuccess = true;
            response.Message = "Todo item successfully updated";
            return response;
        }

        public GetListTodoItemApiView GetUsersTodoItems(ClaimsPrincipal principal)
        {
            var usersTodoItems = new GetListTodoItemApiView
            {
                TodoItems = new List<TodoItemGetListTodoItemApiViewItem>()
            };
            string userId;
            using (_userManager)
            {
                userId = _userManager.GetUserId(principal);
            }
            IEnumerable<TodoItem> retrievedTodoItems = _todoItemRepository.GetUsersTodoItems(userId);
            foreach (TodoItem todoItem in retrievedTodoItems)
            {
                var usersTodoItem = new TodoItemGetListTodoItemApiViewItem
                {
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    IsDone = todoItem.IsDone
                };
                usersTodoItems.TodoItems.Add(usersTodoItem);
            }
            return usersTodoItems;
        }

        public ResponseCreateTodoItemApiView Create(RequestCreateTodoItemApiView todoItem, ClaimsPrincipal principal)
        {
            var response = new ResponseCreateTodoItemApiView();
            if (string.IsNullOrEmpty(todoItem.Name))
            {
                response.Message = "Todo item name cannot be empty";
                return response;
            }
            if (string.IsNullOrEmpty(todoItem.Description))
            {
                response.Message = "Todo item description can not be empty";
                return response;
            }
            string userId;
            using (_userManager)
            {
                userId = _userManager.GetUserId(principal);
            }
            var newTodoItem = new TodoItem
            {
                Name = todoItem.Name,
                Description = todoItem.Description,
                IsDone = todoItem.IsDone,
                UserId = userId
            };
            _todoItemRepository.Insert(newTodoItem);
            response.IsSuccess = true;
            response.Message = "Todo item successfully created";
            return response;
        }

        public GetTodoItemApiView GetTodoItem(int id)
        {
            TodoItem retrievedTodoItem = _todoItemRepository.FindById(id);
            if (retrievedTodoItem == null)
            {
                return null;
            }
            var todoItem = new GetTodoItemApiView
            {
                Name = retrievedTodoItem.Name,
                Description = retrievedTodoItem.Description,
                IsDone = retrievedTodoItem.IsDone
            };
            return todoItem;
        }

        public new DeleteTodoItemApiView Delete(int id)
        {
            var response = new DeleteTodoItemApiView();
            _todoItemRepository.Delete(id);
            response.IsSuccess = true;
            response.Messsage = "Todo item deleted";
            return response;
        }
    }
}
