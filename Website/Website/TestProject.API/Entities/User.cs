using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.API.Entities
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
    }
}
