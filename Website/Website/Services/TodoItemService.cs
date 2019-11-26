﻿using DataAccess.Context;
using Entities;
using Repositories;
using System.Collections.Generic;
using ViewModels.Api.TodoItem;

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

        public GetListTodoItemApiView GetUsersTodoItems(int userId)
        {
            var usersTodoItems = new GetListTodoItemApiView
            {
                TodoItems = new List<TodoItemGetListTodoItemApiViewItem>()
            };
            IEnumerable<TodoItem> retrievedTodoItems = _todoItemRepository.GetUsersTodoItems(userId);
            foreach (TodoItem todoItem in retrievedTodoItems)
            {
                var usersTodoItem = new TodoItemGetListTodoItemApiViewItem
                {
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    IsDone = todoItem.IsDone
                };
                usersTodoItems.TodoItems.Add(usersTodoItem);
            }
            return usersTodoItems;
        }

        public ResponseCreateTodoItemApiView Insert(RequestCreateTodoItemApiView todoItem)
        {
            var response = new ResponseCreateTodoItemApiView();
            if (string.IsNullOrEmpty(todoItem.Name))
            {
                response.Message = "Todo item name cannot be empty";
                return response;
            }
            if (string.IsNullOrEmpty(todoItem.Description))
            {
                response.Message = "Todo item description can not be empty";
                return response;
            }
            var todoItemToInsert = new TodoItem
            {
                Name = todoItem.Name,
                Description = todoItem.Description,
                IsDone = todoItem.IsDone,
                UserId = todoItem.UserId
            };
            base.Insert(todoItemToInsert);
            response.IsSuccess = true;
            response.Message = "Todo item successfully created";
            return response;
        }
    }
}
