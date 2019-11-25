using System.ComponentModel.DataAnnotations;
using TestProject.Resources;

namespace TestProject.ApiModels.User
{
    public class RequestEditNameUserApiModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings),
            ErrorMessageResourceName = nameof(Strings.EmptyUserNameMessage))]
        public string Name { get; set; }
    }
}
