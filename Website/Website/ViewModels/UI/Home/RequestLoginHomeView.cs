using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.Home
{
    public class RequestLoginHomeView
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
