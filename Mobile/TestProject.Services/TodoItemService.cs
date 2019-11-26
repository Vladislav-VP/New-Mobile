using System.Threading.Tasks;

using TestProject.ApiModels.TodoItem;
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

        public async Task<ResponseEditTodoItemApiModel> EditTodoItem(RequestEditTodoItemApiModel todoItem)
        {
            ResponseEditTodoItemApiModel response = await 
                Post<RequestEditTodoItemApiModel, ResponseEditTodoItemApiModel>(todoItem, $"{_url}/Edit");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
            }
            return response;  
        }

        public async Task<GetListTodoItemApiModel> GetUsersTodoItems(int userId)
        {
            GetListTodoItemApiModel usersTodoItems = 
                await Get<GetListTodoItemApiModel>(userId, $"{_url}/GetUsersTodoItems/userId={userId}");
            return usersTodoItems;
        }

        public async Task<GetTodoItemApiModel> GetTodoItem(int id)
        {
            GetTodoItemApiModel todoItem = await Get<GetTodoItemApiModel>(id, $"{_url}/Get/{id}");
            return todoItem;
        }
    }
}
