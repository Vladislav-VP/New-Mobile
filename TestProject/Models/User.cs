using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TestProject.Entities
{
    public class User : BaseEntity
    {
        [Unique, NotNull]
        public string UserName { get; set; }

        [NotNull]
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            User user = (User)obj;
            return this.UserName == user.UserName && this.Password == user.Password;
        }

        public override int GetHashCode()
        {
            var hashCode = 1155857689;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
    }
}
