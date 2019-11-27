using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI
{
    public class RequestLoginUserView
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
