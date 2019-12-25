using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess
{
    public static class DependencyManager
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoListContext>();
            services.AddIdentity<User, IdentityRole>
                (options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<TodoListContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
