using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;

namespace TestProject.Core.Services.Interfaces
{
    public interface IDBService
    {
        Task<string> CreateDatabase();

        Task<UserModel> FindUser(object userPK);

        Task AddUser(UserModel user);

        Task<List<TodoItemModel>> GetTodoItemModels(UserModel user);
    }
}
