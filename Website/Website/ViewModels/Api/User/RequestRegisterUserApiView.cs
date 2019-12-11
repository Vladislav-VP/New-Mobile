using System.ComponentModel.DataAnnotations;

using Constants;

namespace ViewModels.Api.User
{
    public class RequestRegisterUserApiView
    {
        public string Name { get; set; }

        [RegularExpression(UserConstants.PasswordPattern, ErrorMessage = "Invalid character in password: white space")]
        public string Password { get; set; }
    }
}
