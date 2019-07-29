using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public async Task<IEnumerable<TodoItem>> GetTodoItems(int userId)
        {
            var todoItems = await GetAllObjects<TodoItem>();
            return todoItems.Where(todoItem => todoItem.UserId == userId);
        }
    }
}
