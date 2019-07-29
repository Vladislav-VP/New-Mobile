using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestProject.Entities.Resources;

namespace TestProject.Entities
{
    public class TodoItem : BaseEntity
    {
        [NotNull]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.EmptyNameMessage)]
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
