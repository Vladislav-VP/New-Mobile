using DataAccess.Context;
using Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureExternalServices(IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>();
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<TodoListContext>()
            .AddDefaultTokenProviders();
        }
    }
}
