using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using Entities;

namespace DataAccess.Context
{
    public class TodoListContext : IdentityDbContext<User>
    {
        public DbSet<TodoItem> TodoItems { get; set; }

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
