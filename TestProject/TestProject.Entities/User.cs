using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SQLite;
using TestProject.Resources;
using TestProject.Entities.Attributes;

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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            User user = (User)obj;
            return this.Name == user.Name && this.Password == user.Password;
        }

        public override int GetHashCode()
        {
            var hashCode = 1155857689;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
    }
}
