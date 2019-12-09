using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Api;
using Services.Interfaces;
using Services.UI;

namespace Services
{
    public static class DependencyManager
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.DependencyManager.ConfigureServices(services, configuration);
            services.AddTransient<IUsersService, UsersService>();  
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IUsersApiService, UsersApiService>();
            services.AddTransient<ITodoItemApiService, TodoItemApiService>();
            services.AddTransient<ISecurityService, SecurityService>();
        }
    }
}
