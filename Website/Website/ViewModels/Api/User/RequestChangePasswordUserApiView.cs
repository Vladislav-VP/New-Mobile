using System.ComponentModel.DataAnnotations;

using Constants;

namespace ViewModels.Api.User
{
    public class RequestChangePasswordUserApiView
    {
        public string OldPassword { get; set; }

        [RegularExpression(UserConstants.PasswordPattern, ErrorMessage = "Invalid character in password: white space")]
        public string NewPassword { get; set; }
    }
}
