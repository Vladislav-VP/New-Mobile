using DataAccess.Context;
using Entities;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class TodoItemService : BaseService<TodoItem>
    {
        private TodoItemRepository _todoItemRepository;

        public TodoItemService(TodoListContext context)
            : base(context)
        {
            _todoItemRepository = new TodoItemRepository(_context);
        }

        public override void Update(TodoItem todoItem)
        {            
            _todoItemRepository.Update(todoItem);
        }

        public void EditTodoItem(TodoItem todoItem)
        {
            TodoItem todoItemToModify = _todoItemRepository.Find(todoItem.Id);
            if (todoItemToModify == null)
            {
                return;
            }

            todoItemToModify.Description = todoItem.Description;
            todoItemToModify.IsDone = todoItem.IsDone;
            Update(todoItemToModify);
        }

        public IEnumerable<TodoItem> GetUsersTodoItems(int userId)
        {
            return _todoItemRepository.GetUsersTodoItems(userId);
        }
    }
}
