using System.ComponentModel.DataAnnotations;
using SQLite;

using TestProject.Entities.Attributes;
using TestProject.Resources;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        [Unique, NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyNameMessage)]
        public string Name { get; set; }

        [NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyPasswordMessage)]
        [Password(nameof(Password))]
        public string Password { get; set; }

        public string ProfilePhotoInfo { get; set; }
    }
}
