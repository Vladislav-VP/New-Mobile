using System.ComponentModel.DataAnnotations;

using Constants;

namespace ViewModels.UI.User
{
    public class RequestResetPasswordUserView
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        [RegularExpression(UserConstants.PasswordPattern, ErrorMessage = "Invalid format of password")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string NewPasswordConfirmation { get; set; }
    }
}
