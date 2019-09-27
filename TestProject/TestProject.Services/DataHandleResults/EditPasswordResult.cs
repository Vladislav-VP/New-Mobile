using System.ComponentModel.DataAnnotations;

using TestProject.Entities;
using TestProject.Resources;

namespace TestProject.Services.DataHandleResults
{
    public class EditPasswordResult : BaseHandleResult<User>
    {
        private const int _minPasswordLength = 6;
        private const string _passwordCharacterPattern = @"\w+";

        public string OldPassword
        {
            get => Data.Password;
        }

        [Compare(nameof(OldPassword),
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.IncorrectOldPasswordMessage))]
        public string OldPasswordConfirmation { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [MinLength(_minPasswordLength,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.TooShortPasswordMessage))]
        [RegularExpression(_passwordCharacterPattern,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), 
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.PasswordsNotCorrespondMessage))]
        public string NewPasswordConfirmation { get; set; }
    }
}
