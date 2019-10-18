using Microsoft.EntityFrameworkCore;

using TestProject.API.Entities;

namespace TestProject.API.Context
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
