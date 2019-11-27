using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using Entities;

namespace DataAccess.Context
{
    public class TodoListContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configurationBuilder = new ConfigurationBuilder();
            string currentDirectory = Directory.GetCurrentDirectory();
            configurationBuilder.SetBasePath(currentDirectory);
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration config = configurationBuilder.Build();
            string connection = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
