using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entity;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private IGenericRepository<TodoItem> _repository;

        public TodoItemRepository()
        {
            _repository = new GenericRepository<TodoItem>();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(User user)
        {
            var todoItemsTable = _repository.GetTable<TodoItem>();
            return await todoItemsTable.Where(item => item.UserId == user.Id).ToListAsync();
        }
    }
}
