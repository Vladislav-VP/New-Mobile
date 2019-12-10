using System.Threading.Tasks;

using TestProject.ApiModels.TodoItem;

namespace TestProject.Services.Interfaces
{
    public interface ITodoItemService : IBaseApiService
    {
        Task<GetListTodoItemApiModel> GetUsersTodoItems();

        Task<ResponseCreateTodoItemApiModel> CreateTodoItem(RequestCreateTodoItemApiModel todoItem);

        Task<ResponseEditTodoItemApiModel> EditTodoItem(RequestEditTodoItemApiModel todoItem);

        Task<GetTodoItemApiModel> GetTodoItem(int id);

        Task<DeleteTodoItemApiModel> Delete(int id);
    }
}
