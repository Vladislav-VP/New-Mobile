using System.ComponentModel.DataAnnotations;

using TestProject.Resources;

namespace TestProject.ApiModels.TodoItem
{
    public class RequestEditTodoItemApiModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyTodoItemDescriptionMessage))]
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
