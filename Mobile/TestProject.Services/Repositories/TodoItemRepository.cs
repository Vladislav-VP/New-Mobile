using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class TodoItemRepository : BaseRepository<TEntity>, ITodoItemRepository
    {
        public async Task<IEnumerable<TEntity>> GetTodoItems(int userId)
        {
            IEnumerable<TEntity> todoItems = await GetAllObjects();
            return todoItems.Where(todoItem => todoItem.UserId == userId);
        }
    }
}
