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

        //TODO: Refactor method below.
        public override void Update(TodoItem todoItem)
        {            
            TodoItem todoItemToModify = _todoItemRepository.Find(todoItem.Id);
            if (todoItemToModify == null)
            {
                return;
            }

            ModifyTodoItem(todoItemToModify, todoItem);
            _todoItemRepository.Update(todoItemToModify);
        }

        private void ModifyTodoItem(TodoItem oldTodoItem, TodoItem newTodoItem)
        {
            oldTodoItem.Description = newTodoItem.Description;
            oldTodoItem.IsDone = newTodoItem.IsDone;
        }

        public IEnumerable<TodoItem> GetUsersTodoItems(int userId)
        {
            return _todoItemRepository.GetUsersTodoItems(userId);
        }
    }
}
