using System.Collections.Generic;

namespace TestProject.ApiModels.TodoItem
{
    public class GetListTodoItemApiModel
    {
        public List<TodoItemGetListTodoItemApiModelItem> TodoItems { get; set; }
    }

    public class TodoItemGetListTodoItemApiModelItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }
    }
}
