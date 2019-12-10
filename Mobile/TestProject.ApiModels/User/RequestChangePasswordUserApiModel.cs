using System.ComponentModel.DataAnnotations;

using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestChangePasswordUserApiModel
    {
        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), 
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.PasswordsNotCorrespondMessage))]
        public string NewPasswordConfirmation { get; set; }
    }
}
