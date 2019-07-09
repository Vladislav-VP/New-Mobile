using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entity;

namespace TestProject.Repositories.Interfaces
{
    public interface IDBRepository
    {
        Task CreateDatabase();

        Task<User> FindUser(object userPK);

        Task AddUser(User user);

        Task<List<TodoItem>> GetTodoItemModels(User user);
    }
}
