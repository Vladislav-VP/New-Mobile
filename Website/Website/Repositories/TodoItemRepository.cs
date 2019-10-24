using DataAccess.Context;
using Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListContext context) : base(context)
        {
        }
    }
}
