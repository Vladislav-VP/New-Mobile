using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestForgotPasswordUserView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
