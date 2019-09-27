using System.ComponentModel.DataAnnotations;

using SQLite;

using TestProject.Resources;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        private const int _minPasswordLength = 6;
        private const string _passwordCharacterPattern = @"\w+";

        [Unique, NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), 
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }

        [NotNull]
        [Required(ErrorMessageResourceType = typeof(Strings), 
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [MinLength(_minPasswordLength,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.TooShortPasswordMessage))]
        [RegularExpression(_passwordCharacterPattern,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string Password { get; set; }

        public string EncryptedProfilePhoto { get; set; }
    }
}
