using System.Collections.Generic;
using System.Linq;

using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public IEnumerable<TodoItem> GetUsersTodoItems(int userId)
        {
            IEnumerable<TodoItem> todoItems = _dbSet.Where(t => t.User.Id == userId);
            return todoItems;
        }
    }
}
