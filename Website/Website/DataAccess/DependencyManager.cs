using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess
{
    public static class DependencyManager
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TodoListContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        }
    }
}
