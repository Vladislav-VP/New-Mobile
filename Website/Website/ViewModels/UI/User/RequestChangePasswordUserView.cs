using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangePasswordUserView
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        [Required]
        [Compare(nameof(OldPassword))]
        public string OldPasswordConfirmation { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirmation { get; set; }
    }
}
