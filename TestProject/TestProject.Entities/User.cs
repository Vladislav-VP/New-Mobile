using System.ComponentModel.DataAnnotations;

using SQLite;

using TestProject.Entities.Attributes;
using TestProject.Resources;
using TestProject.ValidationConfigurations;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        [Unique, NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }

        [NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [Password(nameof(Password), ValidationConstants.PasswordCharacterPattern, ValidationConstants.MinPasswordLength)]
        public string Password { get; set; }

        public string EncryptedProfilePhoto { get; set; }
    }
}
