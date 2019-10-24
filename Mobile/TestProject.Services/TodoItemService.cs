using TestProject.Entities;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class TodoItemService : BaseApiService<TEntity>, ITodoItemService
    {
        public TodoItemService()
        {
            _url = "http://10.10.3.215:3000/api/todoitems";
        }
    }
}
