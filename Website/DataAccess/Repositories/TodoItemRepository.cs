using System.Collections.Generic;
using System.Linq;

using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListContext context) : base(context)
        {
        }

        public IEnumerable<TodoItem> GetUsersTodoItems(string userId)
        {
            IEnumerable<TodoItem> todoItems = _dbSet.Where(t => t.User.Id == userId);
            return todoItems;
        }
    }
}
