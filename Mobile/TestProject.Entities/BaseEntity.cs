using System;

using SQLite;

namespace TestProject.Entities
{
    public abstract class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}
