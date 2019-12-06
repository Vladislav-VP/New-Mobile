using System.ComponentModel.DataAnnotations;
using TestProject.Configurations;
using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestRegisterUserApiModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        [MinLength(Constants.MinPasswordLength,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.TooShortPasswordMessage))]
        [RegularExpression(Constants.PasswordPattern,
            ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidPasswordFormatMessage))]
        public string Password { get; set; }
    }
}
