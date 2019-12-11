using System.ComponentModel.DataAnnotations;

using TestProject.Resources;

namespace TestProject.ApiModels.TodoItem
{
    public class RequestCreateTodoItemApiModel
    {
        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyTodoItemNameMessage))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyTodoItemDescriptionMessage))]
        public string Description { get; set; }
        
        public bool IsDone { get; set; }
    }
}
