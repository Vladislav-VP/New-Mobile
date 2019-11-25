using System.Collections.Generic;

namespace ViewModels.Api.TodoItem
{
    public class GetListTodoItemApiView
    {
        public List<TodoItemGetListTodoItemApiViewItem> TodoItems { get; set; }
    }

    public class TodoItemGetListTodoItemApiViewItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // TODO : Add user id if necessary.
    }
}
