using System.ComponentModel.DataAnnotations;
using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestEditUserInfoUserApiModel
    {
        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyEmailMessage))]
        [EmailAddress(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.InvalidEmailAddressMessage))]
        public string Email { get; set; }
    }
}
