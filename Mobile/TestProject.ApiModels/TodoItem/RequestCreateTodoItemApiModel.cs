namespace TestProject.ApiModels.TodoItem
{
    public class RequestCreateTodoItemApiModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
