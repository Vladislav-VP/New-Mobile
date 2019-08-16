using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SQLite;
using TestProject.Resources;
using TestProject.Entities.Attributes;
using System.Drawing;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        [Unique, NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyNameMessage)]
        public string Name { get; set; }

        [NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyPasswordMessage)]
        [Password(nameof(Password))]
        public string Password { get; set; }

        public string ProfilePhotoInfo { get; set; }
    }
}
