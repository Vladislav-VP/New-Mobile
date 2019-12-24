using System.ComponentModel.DataAnnotations;

namespace ViewModels.Api.TodoItem
{
    public class RequestEditTodoItemApiView
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
