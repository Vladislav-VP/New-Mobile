using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestLoginUserView
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Username can not be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
