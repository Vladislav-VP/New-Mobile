using System.ComponentModel.DataAnnotations;

namespace ViewModels.Api.TodoItem
{
    public class RequestCreateTodoItemApiView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
