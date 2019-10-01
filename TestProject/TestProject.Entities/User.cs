using System.ComponentModel.DataAnnotations;

using SQLite;

using TestProject.Configurations;
using TestProject.Resources;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        [Unique, NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), 
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }

        [NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), 
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [MinLength(Constants.MinPasswordLength,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.TooShortPasswordMessage))]
        [RegularExpression(Constants.PasswordPattern,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string Password { get; set; }

        public string EncryptedProfilePhoto { get; set; }
    }
}
