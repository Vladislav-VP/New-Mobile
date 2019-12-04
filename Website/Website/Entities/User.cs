using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace Entities
{
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = nameof(Password))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public byte[] ImageBytes { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public User()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
