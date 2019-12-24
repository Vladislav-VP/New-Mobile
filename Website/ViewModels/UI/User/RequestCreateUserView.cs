using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestCreateUserView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
