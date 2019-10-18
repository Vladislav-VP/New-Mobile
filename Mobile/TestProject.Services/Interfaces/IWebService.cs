using System.Collections.Generic;
using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Interfaces
{
    public interface IWebService
    {
        Task<IEnumerable<TodoItem>> Get();

        Task<TodoItem> Add(TodoItem todoItem);

        Task<TodoItem> Update(TodoItem todoItem);

        Task<TodoItem> Delete(int id);
    }
}
