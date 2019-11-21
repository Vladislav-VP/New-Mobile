using DataAccess.Context;
using Entities;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListContext context) : base(context)
        {
        }

        public IEnumerable<TodoItem> GetUsersTodoItems(int userId)
        {
            IEnumerable<TodoItem> todoItems = _dbSet.Where(t => t.User.Id == userId);
            return todoItems;
        }
    }
}
