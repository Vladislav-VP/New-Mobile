using Microsoft.Extensions.DependencyInjection;

//using Services.Api;
using Services.Interfaces;
using Services.UI;

namespace Services
{
    public static class DependencyManager
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            DataAccess.DependencyManager.ConfigureServices(services);
            services.AddTransient<IUsersService, UsersService>();  
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IImageService, ImageService>();
            // TODO : Uncomment after include api back.
            //services.AddTransient<IUsersApiService, UsersApiService>();
            //services.AddTransient<ITodoItemApiService, TodoItemApiService>();
        }
    }
}
