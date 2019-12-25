using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangeEmailUserView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
