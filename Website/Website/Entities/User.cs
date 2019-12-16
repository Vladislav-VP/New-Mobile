using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            CreationDate = DateTime.UtcNow;
        }        
        
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public string ConfirmationToken { get; set; }

        public List<TodoItem> TodoItems { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
