using Microsoft.EntityFrameworkCore;

using Entities;

namespace DataAccess.Context
{
    public class TodoListContext : DbContext
    {
        public TodoListContext() : base()
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // TODO : Implement logic for retrieving connection string from json
            string connection = 
                "Server=(localdb)\\mssqllocaldb;Database=todolistdb;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
