using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace TestProject.Core.Models
{
    public class TodoItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserName { get; set; }
    }
}
