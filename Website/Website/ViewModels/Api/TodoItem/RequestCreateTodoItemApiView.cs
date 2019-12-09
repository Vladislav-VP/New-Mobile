namespace ViewModels.Api.TodoItem
{
    public class RequestCreateTodoItemApiView
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
