using Entities;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        IEnumerable<TodoItem> GetUsersTodoItems(int userId);
    }
}
