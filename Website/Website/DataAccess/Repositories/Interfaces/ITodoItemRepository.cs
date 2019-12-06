using System.Collections.Generic;

using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        IEnumerable<TodoItem> GetUsersTodoItems(string userId);
    }
}
