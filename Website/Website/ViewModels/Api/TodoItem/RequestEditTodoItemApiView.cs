namespace ViewModels.Api.TodoItem
{
    public class RequestEditTodoItemApiView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
