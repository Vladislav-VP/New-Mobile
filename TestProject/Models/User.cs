using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TestProject.Entity
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string UserName { get; set; }

        [NotNull]
        public string Password { get; set; }
    }
}
