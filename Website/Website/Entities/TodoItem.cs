using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class TodoItem : BaseEntity
    {
        [Required]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Required]
        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public override string ToString()
        {
            return $"Name : {Name}, Description : {Description}, Done : {IsDone}";
        }
    }
}
