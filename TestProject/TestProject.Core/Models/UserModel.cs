using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TestProject.Core.Models
{
    public class UserModel
    {
        [PrimaryKey]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
