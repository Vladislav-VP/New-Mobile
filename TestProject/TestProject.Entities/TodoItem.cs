using System.ComponentModel.DataAnnotations;

using SQLite;
using SQLiteNetExtensions.Attributes;
using TestProject.ValidationConfigurations;

namespace TestProject.Entities
{
    public class TodoItem : BaseEntity
    {
        [NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyTodoItemNameMessage)]
        public string Name { get; set; }

        [NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyTodoItemDescriptionMessage)]
        public string Description { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey(typeof(User)), NotNull]
        public int UserId { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
