using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IBaseRepository<TodoItem> _repository;

        public TodoItemRepository()
        {
            _repository = new BaseRepository<TodoItem>();
        }

        public async Task Delete<T>(object pk)
        {
            await _repository.Delete<TodoItem>(pk);
        }

        public async Task<T> Find<T>(object pk) where T : class, new()
        {
            return await _repository.Find<T>(pk);
        }

        public async Task<IEnumerable<T>> GetAllObjects<T>() where T : class, new()
        {
            return await _repository.GetAllObjects<T>();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(int userId)
        {
            var todoItems = await GetAllObjects<TodoItem>();
            return todoItems.Where(todoItem => todoItem.UserId == userId);
        }

        public async Task<bool> Insert(TodoItem item)
        {
            return await _repository.Insert(item);
        }

        public async Task Update(TodoItem item)
        {
            await _repository.Update(item);
        }
    }
}
