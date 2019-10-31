using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class TodoItemService : BaseApiService<TodoItem>, ITodoItemService
    {
        private readonly IValidationHelper _validationHelper;
        public TodoItemService(IValidationHelper validationHelper)
        {
            _url = "http://10.10.3.215:3000/api/todoitems";

            _validationHelper = validationHelper;
        }

        public async Task<IEnumerable<TodoItem>> GetUsersTodoItems(int userId)
        {
            IEnumerable<TodoItem> todoItems = await GetObjectsList($"{_url}/userId={userId}");
            return todoItems;
        }

        public async Task<DataHandleResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            var result = new DataHandleResult<TodoItem>
            {
                Data = todoItem
            };

            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return result;
            }

            await Post(todoItem, _url);
            return result;
        }

        public async Task<DataHandleResult<TodoItem>> EditTodoItem(TodoItem todoItem, string description, bool isDone)
        {
            var result = new DataHandleResult<TodoItem>
            {
                Data = todoItem
            };

            var modifiedTodoItem = new TodoItem
            {
                Name = todoItem.Name,
                Description = todoItem.Description,
                IsDone = todoItem.IsDone
            };
            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return result;
            }

            todoItem.Description = description;
            todoItem.IsDone = isDone;
            result.IsSucceded = true;
            await Update(todoItem);
            return result;
        }
    }
}
