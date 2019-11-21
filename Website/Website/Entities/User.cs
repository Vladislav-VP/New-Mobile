//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class User : BaseEntity
    {
        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; }

        [Required]
        [Display(Name = nameof(Password))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public byte[] ImageBytes { get; set; }
    }
}
