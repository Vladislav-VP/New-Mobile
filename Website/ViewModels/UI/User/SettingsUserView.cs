using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class SettingsUserView
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Profile photo")]
        public string ImageUrl { get; set; }
    }
}
