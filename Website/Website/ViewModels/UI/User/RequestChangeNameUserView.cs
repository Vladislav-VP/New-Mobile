using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangeNameUserView
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
