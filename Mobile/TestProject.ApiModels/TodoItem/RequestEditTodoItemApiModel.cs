namespace TestProject.ApiModels.TodoItem
{
    public class RequestEditTodoItemApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
