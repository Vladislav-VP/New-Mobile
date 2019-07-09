using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace TestProject.Entity
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey(typeof(User))]
        public string UserName { get; set; }
    }
}
