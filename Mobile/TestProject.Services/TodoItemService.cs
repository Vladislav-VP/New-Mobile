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

        private readonly IDialogsHelper _dialogsHelper;

        public TodoItemService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
        {
            _url = "http://10.10.3.215:3000/api/todoitemapi";

            _validationHelper = validationHelper;
            _dialogsHelper = dialogsHelper;
        }

        public async Task<ResponseCreateTodoItemApiModel> CreateTodoItem(RequestCreateTodoItemApiModel todoItem)
        {
            ResponseCreateTodoItemApiModel response = await
                Post<RequestCreateTodoItemApiModel, ResponseCreateTodoItemApiModel>(todoItem, $"{_url}/Create");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
            }
            return response;
        }

        public async Task<ResponseCreateTodoItemApiModel> EditTodoItem(RequestCreateTodoItemApiModel todoItem)
        {
            //var response = new ResponseCreateTodoItemApiModel();
             throw new NotImplementedException();        
        }

        public async Task<GetListTodoItemApiModel> GetUsersTodoItems(int userId)
        {
            GetListTodoItemApiModel usersTodoItems = 
                await Get<GetListTodoItemApiModel>(userId, $"{_url}/GetUsersTodoItems/userId={userId}");
            return usersTodoItems;
        }

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
