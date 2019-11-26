using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.ApiModels.TodoItem;
using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class TodoItemService : BaseApiService, ITodoItemService
    {
        private readonly IValidationHelper _validationHelper;
        public TodoItemService(IValidationHelper validationHelper)
        {
            _url = "http://10.10.3.215:3000/api/todoitemapi";

            _validationHelper = validationHelper;
        }

        public Task<DataHandleResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task<DataHandleResult<TodoItem>> EditTodoItem(TodoItem todoItem, string description, bool isDone)
        {
            throw new NotImplementedException();
        }

        public async Task<GetListTodoItemApiModel> GetUsersTodoItems(int userId)
        {
            GetListTodoItemApiModel usersTodoItems = 
                await Get<GetListTodoItemApiModel>(userId, $"{_url}/GetUsersTodoItems/userId={userId}");
            return usersTodoItems;
        }

        //public async Task<IEnumerable<T>> GetUsersTodoItems(int userId)
        //{
        //    IEnumerable<T> todoItems = await GetObjectsList<T>($"{_url}/GetUsersTodoItems/userId={userId}");
        //    return todoItems;
        //}

        //public async Task<DataHandleResult<T>> CreateTodoItem(T todoItem)
        //{
        //    var result = new DataHandleResult<T>
        //    {
        //        Data = todoItem
        //    };

        //    bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
        //    if (!isTodoItemValid)
        //    {
        //        return result;
        //    }

        //    await Post(todoItem, _url);
        //    return result;
        //}

        //public async Task<DataHandleResult<T>> EditTodoItem(T todoItem, string description, bool isDone)
        //{
        //    var result = new DataHandleResult<T>
        //    {
        //        Data = todoItem
        //    };

        //    var modifiedTodoItem = new T
        //    {
        //        Name = todoItem.Name,
        //        Description = todoItem.Description,
        //        IsDone = todoItem.IsDone
        //    };
        //    bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
        //    if (!isTodoItemValid)
        //    {
        //        return result;
        //    }

        //    todoItem.Description = description;
        //    todoItem.IsDone = isDone;
        //    result.IsSucceded = true;
        //    await Update(todoItem);
        //    return result;
        //}
    }
}
