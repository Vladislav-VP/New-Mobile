using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.ApiModels.TodoItem;
using TestProject.Entities;
using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface ITodoItemService : IBaseApiService
    {
        Task<GetListTodoItemApiModel> GetUsersTodoItems(int userId);

        Task<DataHandleResult<TodoItem>> CreateTodoItem(TodoItem todoItem);

        Task<DataHandleResult<TodoItem>> EditTodoItem(TodoItem todoItem, string description, bool isDone);
    }
}
