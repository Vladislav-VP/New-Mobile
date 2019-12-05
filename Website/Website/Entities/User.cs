using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User : IdentityUser
    {
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public User()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
