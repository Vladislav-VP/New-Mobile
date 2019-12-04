using Entities;
using ViewModels.Api.TodoItem;

namespace Services.Interfaces
{
    public interface ITodoItemApiService : IBaseApiService<TodoItem>
    {
        ResponseEditTodoItemApiView EditTodoItem(RequestEditTodoItemApiView todoItem);

        GetListTodoItemApiView GetUsersTodoItems(string userId);

        ResponseCreateTodoItemApiView Insert(RequestCreateTodoItemApiView todoItem);

        GetTodoItemApiView GetTodoItem(int id);

        new DeleteTodoItemApiView Delete(int id);
    }
}
