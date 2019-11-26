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

        Task<ResponseCreateTodoItemApiModel> CreateTodoItem(RequestCreateTodoItemApiModel todoItem);

        Task<ResponseEditTodoItemApiModel> EditTodoItem(RequestEditTodoItemApiModel todoItem);

        Task<GetTodoItemApiModel> GetTodoItem(int id);
    }
}
