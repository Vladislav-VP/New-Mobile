using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace TestProject.Entities
{
    public abstract class BaseEntity
    {
        //[PrimaryKey, AutoIncrement]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}
