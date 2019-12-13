using System.ComponentModel.DataAnnotations;

using TestProject.Configurations;
using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestRegisterUserApiModel
    {
        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [RegularExpression(Constants.PasswordPattern, ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string Password { get; set; }
    }
}
