using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI
{
    public class RequestRegisterUserView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
