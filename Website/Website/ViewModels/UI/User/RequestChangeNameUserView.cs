using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangeNameUserView
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
