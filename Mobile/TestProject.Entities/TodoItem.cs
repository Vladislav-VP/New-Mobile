using System.ComponentModel.DataAnnotations;

using SQLite;
using SQLiteNetExtensions.Attributes;

using TestProject.Resources;

namespace TestProject.Entities
{
    public class TodoItem : BaseEntity
    {
        [NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.EmptyTodoItemNameMessage))]
        public string Name { get; set; }

        [NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.EmptyTodoItemDescriptionMessage))]
        public string Description { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey(typeof(TodoItem)), NotNull]
        public int UserId { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
