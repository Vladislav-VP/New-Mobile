using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangePasswordUserView
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirmation { get; set; }
    }
}
