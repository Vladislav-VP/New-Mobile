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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
            builder.Entity<TodoItem>()
                .HasOne(p => p.User)
                .WithMany(t => t.TodoItems)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RefreshToken>()
                .HasOne(p => p.User)
                .WithMany(t => t.RefreshTokens)
                .OnDelete(DeleteBehavior.Cascade);
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
