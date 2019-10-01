using System.ComponentModel.DataAnnotations;
using TestProject.Configurations;
using TestProject.Entities;
using TestProject.Resources;

namespace TestProject.Services.DataHandleResults
{
    public class EditPasswordResult : BaseHandleResult<User>
    {
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
