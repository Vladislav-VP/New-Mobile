using System.Threading.Tasks;

using TestProject.ApiModels.TodoItem;

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
