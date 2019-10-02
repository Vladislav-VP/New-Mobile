using System.ComponentModel.DataAnnotations;

using TestProject.Configurations;
using TestProject.Resources;

namespace TestProject.Services.Helpers
{
    public class EditPasswordHelper
    {
        public string OldPassword { get; set; }

        [Compare(nameof(OldPassword),
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.IncorrectOldPasswordMessage))]
        public string OldPasswordConfirmation { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [MinLength(Constants.MinPasswordLength,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.TooShortPasswordMessage))]
        [RegularExpression(Constants.PasswordPattern,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword),
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.PasswordsNotCorrespondMessage))]
        public string NewPasswordConfirmation { get; set; }
    }
}
