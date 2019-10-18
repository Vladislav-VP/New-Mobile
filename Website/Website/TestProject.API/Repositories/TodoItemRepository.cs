using TestProject.API.Context;
using TestProject.API.Entities;
using TestProject.API.Repositories.Interfaces;

namespace TestProject.API.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListContext context) : base(context)
        {
        }
    }
}
