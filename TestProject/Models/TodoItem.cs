using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Entities
{
    public class TodoItem : BaseEntity
    {
        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey(typeof(User)), NotNull]
        public int UserId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
