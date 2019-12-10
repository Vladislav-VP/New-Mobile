using System.ComponentModel.DataAnnotations;

using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestRegisterUserApiModel
    {
        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyPasswordMessage))]
        public string Password { get; set; }
    }
}
