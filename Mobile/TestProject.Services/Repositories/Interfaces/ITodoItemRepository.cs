using System.Collections.Generic;
using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetTodoItems(int userId);
    }
}
